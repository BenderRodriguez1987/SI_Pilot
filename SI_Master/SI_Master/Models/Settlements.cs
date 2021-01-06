using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Settlements
    {
        [JsonProperty("items")]
        public List<Settlement> SettlementsList { get; set; }

        [JsonProperty("totals")]
        public Totals Totals { get; set; }

        [JsonProperty("total2")]
        public Total2 Total2 { get; set; }
    }
}
