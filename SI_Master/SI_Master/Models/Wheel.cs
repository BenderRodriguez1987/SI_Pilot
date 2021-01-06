using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class Wheel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("works")]
        public List<WheelWorkItem> Works { get; set; }

    }

    public class WheelWorkItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
