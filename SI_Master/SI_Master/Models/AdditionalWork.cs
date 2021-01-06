using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class AdditionalWork
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }
    }
}
