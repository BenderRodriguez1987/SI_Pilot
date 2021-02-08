using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class Response<T>
    {

        [JsonProperty("real")]
        public bool Real { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
