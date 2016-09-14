using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SampleApp.Services.Client.Elasticsearch
{
    public enum SearchEntityType
    {
        None = 0,
        UserName = 1,
        TagName = 2
    }
}
