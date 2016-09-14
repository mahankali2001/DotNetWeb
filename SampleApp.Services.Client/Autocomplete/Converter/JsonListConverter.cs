using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace SampleApp.Services.Client.Elasticsearch.Entities
{
    public class JsonListConverter<T> : CustomCreationConverter<IList<T>>
    {
        public override IList<T> Create(Type objectType)
        {
            return new List<T>();
        }

        public override object ReadJson(JsonReader reader,  Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                // If already an array, simply deserialize it
                object result = serializer.Deserialize(reader, objectType);
                return result;
            }
            else
            {
                // If not an array, deserialize and convert to List
                var result = serializer.Deserialize<T>(reader);
                var list = new List<T>() { result };
                return list;
            }
        }
    }
}
