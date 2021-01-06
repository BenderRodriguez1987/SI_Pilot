using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Limits
    {
        [JsonProperty("fuel_type")]
        public string FuelType { get; set; }

        [JsonProperty("fuel_amount")]
        public float FuelAmount { get; set; }

        [JsonProperty("fuel_units")]
        public string FuelUnits { get; set; }

        [JsonProperty("limit_type")]
        public string LimitType { get; set; }

        [JsonIgnore]
        public string FuelString
        {
            get
            {
                return FuelAmount + " " + FuelUnits;
            }
        }
    }
}
