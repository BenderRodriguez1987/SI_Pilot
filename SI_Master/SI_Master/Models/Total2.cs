using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Total2
    {
        [JsonProperty("sum")]
        public float Sum { get; set; }

        [JsonProperty("fuel_amount")]
        public float FuelAmount { get; set; }
    }
}
