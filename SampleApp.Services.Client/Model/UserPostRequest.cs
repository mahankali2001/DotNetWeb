using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp.Services.Client.Model
{
    public class UserRequest
    {
        public int uid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class UserResponse
    {
        public int uid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string errorCode { get; set; }
    }
}