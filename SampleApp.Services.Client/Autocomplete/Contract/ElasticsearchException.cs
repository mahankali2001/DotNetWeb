using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp.Services.Client.Elasticsearch
{
    public class ElasticsearchException : ApplicationException
    {
        public ElasticsearchException(string message, ESFaultType fault):base(message)
        {
            this.Fault = fault;
        }

        public ESFaultType Fault { get; private set; }
    }

    public enum ESFaultType
    {
        None = 0,
        ElasticsearchConnectionError = 1,
        ElasticsearchResponseError = 2,
    }
}
