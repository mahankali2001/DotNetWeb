
namespace SampleApp.Services.Client.Elasticsearch
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using SampleApp.Services.Client.Elasticsearch.Entities;
    using SampleApp.Services.Client.Elasticsearch.Extensions;
    using Nest;

    public partial class ElasticsearchClient
    {
        //protected static readonly SampleApp.Core.Logger.ILogger Logger = SampleApp.Core.Logger.LogManager.GetLogger(typeof(ElasticsearchClient));

        private static readonly string ESUri;
        private static readonly string ElasticsearchIndexName;
        private static readonly int ESMaxAutoCompleteResults;
        private static readonly int ESBatchSize;

        static ElasticsearchClient()
        {
            ESUri = ConfigurationManager.AppSettings["ESUri"];
            ElasticsearchIndexName = ConfigurationManager.AppSettings["RetailerCode"].ToLowerInvariant();
            ESMaxAutoCompleteResults = GetIntValue(ConfigurationManager.AppSettings["ESMaxAutoCompleteResults"], 10);
            ESBatchSize = GetIntValue(ConfigurationManager.AppSettings["ESBatchSize"], 250);
        }

        public List<string> AutoComplete(SearchFieldType fieldType, string searchText, int[] Ids)
        {
            if (fieldType == SearchFieldType.None)
            {
                throw new ArgumentException("Invalid Parameter", "fieldType");
            }

            if (string.IsNullOrEmpty(searchText))
            {
                throw new ArgumentException("Invalid Parameter", "searchText");
            }

            Ids = Ids ?? new int[] { };
            return this.BuildQueryAndExecuteSearch(fieldType, searchText, Ids);
        }
       
        public void DeleteEntities(SearchEntityType entityType, string[] ids)
        {
            if (entityType == SearchEntityType.None)
            {
                throw new ArgumentException("Invalid Parameter", "entityType");
            }

            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException("Empty Parameter", "ids");
            }

            switch (entityType)
            {

                case SearchEntityType.UserName:
                    this.DeleteEntities<User>(ids);
                    break;

                case SearchEntityType.TagName:
                    this.DeleteEntities<Tag>(ids);
                    break;
            }
        }

        public string GetESAliasStatus()
        {
            var esNEST = GetESClient();

            var rootInfoResponse = esNEST.RootNodeInfo(new InfoRequest());
            if (!rootInfoResponse.IsValid)
            {
                throw new Exception(string.Format("Response invalid connecting to Elasticsearch"));
            }

            var healthResponse = esNEST.ClusterHealth();
            if (!healthResponse.IsValid)
            {
                throw new Exception(string.Format("Response invalid retrieving cluster health"));
            }

            bool isClusterOk = false;
            if (String.Equals("green", healthResponse.Status, StringComparison.OrdinalIgnoreCase) || String.Equals("yellow", healthResponse.Status, StringComparison.OrdinalIgnoreCase))
            {
                isClusterOk = true;
            }

            bool isAliasIndexLoaded = false;
            var startResponse = esNEST.Get<MarkerRecord>("start");
            var endResponse = esNEST.Get<MarkerRecord>("end");
            if (!startResponse.Found || !endResponse.Found)
            {
                isAliasIndexLoaded = false;
            }
            else
            {
                isAliasIndexLoaded = true;
            }

            string message = string.Empty;
            if (isClusterOk && isAliasIndexLoaded)
            {
                message = string.Format("Alias has data, Cluster '{0}'", healthResponse.Status);
            }
            else if (!isClusterOk && isAliasIndexLoaded)
            {
                throw new Exception(string.Format("Alias may have outdated data, Cluster '{0}'", healthResponse.Status));
            }
            else
            {
                throw new Exception(string.Format("Alias doesn't have data, Cluster '{0}'", healthResponse.Status));
            }

            return message;
        }

        private static ElasticClient GetESClient()
        {
            var setting = new ConnectionSettings(new Uri(ESUri));
            setting.SetDefaultIndex(ElasticsearchIndexName);
            ElasticClient client = new ElasticClient(setting);
            return client;
        }

        private static int GetIntValue(string value, int defaultValue)
        {
            int parsedValue;
            bool isParsed = int.TryParse(value, out parsedValue);
            return isParsed ? parsedValue : defaultValue;
        }

        private List<string> BuildQueryAndExecuteSearch(SearchFieldType fieldType, string searchText, int[] Ids)
        {
            List<string> results = null;
            switch (fieldType)
            {
                case SearchFieldType.UserName:
                    var userQuery = BuildQuery<User>(r => r.Name.Suffix("autocomplete"), searchText,
                        r => r.Name,
                        r => r.Ids, Ids.Select(p => (object)p).ToArray());
                    results = ExecuteSearch(userQuery, r => r.Name.FirstOrDefault());
                    break;

                case SearchFieldType.TagName:
                    var tagQuery = BuildQuery<Tag>(r => r.Name.Suffix("autocomplete"), searchText,
                        r => r.Name,
                        r => r.Ids, Ids.Select(p => (object)p).ToArray());
                    results = ExecuteSearch(tagQuery, r => r.Name.FirstOrDefault());
                    break;
            }
            return results;
        }

        private List<string> ExecuteSearch<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> query, Expression<Func<T, object>> projection)
            where T : EntityBase
        {
            try
            {
                ElasticClient client = GetESClient();
                var response = client.Search<T>(query);
                if (!response.IsValid)
                {
                    throw new ElasticsearchException("Invalid response from Elasticsearch", ESFaultType.ElasticsearchResponseError);
                }


                List<string> results = new List<string>(); 
                
                if(response.Hits == null) 
                {
                    return results;
                }

                foreach (var hit in response.Hits)
                {
                    string [] fieldValues = hit.Fields.FieldValues<T, string>(projection);
                    if (fieldValues == null || fieldValues.Length == 0)
                    {
                        continue;
                    }

                    results.Add(fieldValues[0]);
                }

                return results;
            }
            catch (ElasticsearchException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElasticsearchException(ex.Message, ESFaultType.ElasticsearchConnectionError);
            }
        }

        private Func<SearchDescriptor<T>, SearchDescriptor<T>> BuildQuery<T>(Expression<Func<T, object>> queryField, string queryValue,
                Expression<Func<T, object>> retrieveField,
                Expression<Func<T, object>> filterField,  ICollection<object> filterValues) where T : EntityBase
        {
            var matchQuery = Query<T>.Match(t => t.OnField(queryField).Query(queryValue));

            FilterContainer filter = null;
            if (filterValues != null && filterValues.Count != 0)
            {
                filter = Filter<T>.Terms(filterField, filterValues.Select(v => v.ToString()));
            }

            Func<SearchDescriptor<T>,SearchDescriptor<T>> searchDescriptior = null;
            int size = ESMaxAutoCompleteResults;
            if (filter != null)
            {
                searchDescriptior = s => s.From(0).Size(size).Query(q => matchQuery).Filter(filter).Fields(retrieveField);
            }
            else
            {
                searchDescriptior = s => s.From(0).Size(size).Query(q => matchQuery).Fields(retrieveField);
            }
            return searchDescriptior;
        }

        private void MergeCustomerMapping<T>(ICollection<string> uniqueIds, int[] Ids,
            bool isAdd,
            Func<string, T> instantiateWithData = null) where T : EntityBaseMultiCustomer
        {
            if (uniqueIds.Count == 0) return;

            IEnumerable<T> existingEntities = null;
            ElasticClient client = null;
            try
            {
                client = GetESClient();
                var results = client.MultiGet(m => m.GetMany<T>(uniqueIds));
                results.MapMetaDataToObject<T>();
                existingEntities = results.Documents.OfType<MultiGetHit<T>>().Where(m => m.Source != null).Select(m => m.Source).ToList();
            }
            catch (ElasticsearchException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElasticsearchException(ex.Message, ESFaultType.ElasticsearchConnectionError);
            }

            List<T> modified = null;
            List<T> deleted = null;
            if (isAdd)
            {
                this.AddCustomerMapping<T>(uniqueIds, existingEntities, Ids, instantiateWithData, out modified);
            }
            else
            {
                this.RemoveCustomerMapping<T>(existingEntities, Ids, out modified, out deleted);
            }

            this.UpdateEntitiesInES<T>(client, modified, deleted);
        }

        private void UpdateEntitiesInES<T>(ElasticClient client, List<T> updateSet, List<T> deleteSet = null) where T : EntityBaseMultiCustomer
        {
            try
            {
                if (updateSet != null && updateSet.Count > 0)
                {
                    foreach (var batch in updateSet.Batch(ESBatchSize))
                    {
                        var result = client.IndexMany(batch);
                        if (!result.IsValid)
                        {
                            throw new ElasticsearchException("Invalid response from Elasticsearch", ESFaultType.ElasticsearchResponseError);
                        }
                    }
                }

                if (deleteSet != null && deleteSet.Count > 0)
                {
                    foreach (var batch in deleteSet.Batch(ESBatchSize))
                    {
                        var result = client.DeleteMany<T>(batch);
                        if (!result.IsValid)
                        {
                            throw new ElasticsearchException("Invalid response from Elasticsearch", ESFaultType.ElasticsearchResponseError);
                        }
                    }
                }
            }
            catch (ElasticsearchException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElasticsearchException(ex.Message, ESFaultType.ElasticsearchConnectionError);
            }
        }

        private List<T> AddCustomerMapping<T>(IEnumerable<string> uniqueIds, IEnumerable<T> existingEntities,
            int[] Ids, Func<string, T> instantiate, out List<T> modified) where T : EntityBaseMultiCustomer
        {
            modified = new List<T>();
            var map = new Dictionary<string, T>();
            foreach (T entity in existingEntities)
            {
                map.Add(entity.Id, entity);
            }

            foreach (var entityId in uniqueIds)
            {
                if (map.ContainsKey(entityId))
                {
                    // ES already contains the entity
                    // So we just need to make sure entity's RetailerPartyIds contains the Retailers specified
                    T existingEntity = map[entityId];
                    if (existingEntity.Ids == null
                        || Ids.Any(r => !existingEntity.Ids.Contains(r)))
                    {
                        existingEntity.Ids = existingEntity.Ids ?? new List<int>();
                        var retailersToAdd = Ids.Where(r => !existingEntity.Ids.Contains(r));
                        existingEntity.Ids.AddRange(retailersToAdd);
                        modified.Add(existingEntity);
                    }
                }
                else
                {
                    // ES doesn't contain the specific item
                    T newEntity = instantiate(entityId);
                    newEntity.Id = entityId;
                    newEntity.Ids = new List<int>(Ids);
                    modified.Add(newEntity);
                }
            }
            return modified;
        }

        private void RemoveCustomerMapping<T>(IEnumerable<T> existingEntities,
            int[] Ids, out List<T> modified, out List<T> deleted) where T : EntityBaseMultiCustomer
        {
            modified = new List<T>();
            deleted = new List<T>();
            foreach (var entity in existingEntities)
            {
                int removedCount = 0;
                if (entity.Ids != null)
                {
                    removedCount = entity.Ids.RemoveAll(r => Ids.Contains(r));
                }

                if (entity.Ids == null || entity.Ids.Count == 0)
                {
                    // No retailers left in the entity
                    // So it can be deleted from ES
                    deleted.Add(entity);
                }
                else if (removedCount > 0)
                {
                    // Some retailers were removed, but the entity still has other retailers left
                    // So entity needs to be updated in ES
                    modified.Add(entity);
                }
            }
        }

        private void DeleteEntities<T>(string[] ids) where T : EntityBase
        {
            try
            {
                ElasticClient client = GetESClient();
                var uniqueIds = ids.Distinct();
                foreach (var id in uniqueIds)
                {
                    var result = client.Delete<T>(id);
                    if (!result.IsValid)
                    {
                        throw new ElasticsearchException("Invalid response from Elasticsearch", ESFaultType.ElasticsearchResponseError);
                    }
                }
            }
            catch (ElasticsearchException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ElasticsearchException(ex.Message, ESFaultType.ElasticsearchConnectionError);
            }
        }

    }
}
