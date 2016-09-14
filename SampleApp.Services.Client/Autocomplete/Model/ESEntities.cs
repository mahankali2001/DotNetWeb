using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Newtonsoft.Json;

namespace SampleApp.Services.Client.Elasticsearch.Entities
{
    public abstract class EntityBase
    {
        [JsonIgnore]
        public string Id { get; set; }
    }

    public abstract class EntityBaseSingleCustomer : EntityBase
    {
        [ElasticProperty(Name = "Id")]
        public int Id { get; set; }
    }

    public abstract class EntityBaseMultiCustomer : EntityBase
    {
        [ElasticProperty(Name = "Id")]
        [JsonConverter(typeof(JsonListConverter<int>))]
        public List<int> Ids { get; set; }
    }

    [ElasticType(Name = "User")]
    public class User : EntityBaseMultiCustomer
    {
        [ElasticProperty(Name = "Name")]
        [JsonConverter(typeof(JsonListConverter<string>))]
        public List<string> Name { get; set; }
    }

    [ElasticType(Name = "Tag")]
    public class Tag : EntityBaseMultiCustomer
    {
        [ElasticProperty(Name = "Name")]
        [JsonConverter(typeof(JsonListConverter<string>))]
        public List<string> Name { get; set; }
    }

    [ElasticType(Name = "MarkerRecord")]
    public class MarkerRecord
    {
        public string Data { get; set; }
    }
}
