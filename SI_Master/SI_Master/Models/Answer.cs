using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class Answer
    {
        [JsonProperty(PropertyName = "real")]
        public bool IsReal { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string ResMsg { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Object ResData { get; set; }

        //[JsonProperty(PropertyName = "result")]
        public int  ResType { get; set; }
    }
}
