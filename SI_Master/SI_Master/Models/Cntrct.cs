using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Cntrct
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("blocked")]
        public bool Blocked { get; set; }

        [JsonProperty("sum")]
        public float Sum { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("charge")]
        public float Change { get; set; }
    }
}
