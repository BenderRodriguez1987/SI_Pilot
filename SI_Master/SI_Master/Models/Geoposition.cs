using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class Geoposition
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }
    }
}
