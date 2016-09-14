using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using SampleApp.Services.Client.Elasticsearch.Entities;

namespace SampleApp.Services.Client.Elasticsearch.Extensions
{
    static class IMultiGetResponseExtensions
    {
        internal static void MapMetaDataToObject<T>(this IMultiGetResponse multiGetResponse) 
            where T : EntityBase
        {
            IEnumerable<MultiGetHit<T>> hits = multiGetResponse.Documents.OfType<MultiGetHit<T>>();
            foreach (var hit in hits)
            {
                if (hit.Source != null)
                {
                    hit.Source.Id = hit.Id;
                }
            }
        }
    }
}
