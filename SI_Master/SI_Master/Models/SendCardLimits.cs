using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class SendCardLimits
    {
        [JsonProperty("limits")]
        public Dictionary<string, List<Limits>> Limits { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("refuelling_stations")]
        public string RefuelingStations { get; set; }
    }
}
