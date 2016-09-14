using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace SampleApp.Services.Client.Elasticsearch.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            if (batchSize < 1)
            {
                throw new ArgumentException("batchSize");
            }

            List<T> batch = new List<T>(batchSize);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<T>(batchSize);
                }
            }
            if (batch.Any())
            {
                yield return batch;
            }
        }
    }
}
