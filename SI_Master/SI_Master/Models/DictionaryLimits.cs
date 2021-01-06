using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class DictionaryLimits: ICloneable
    {
        public List<Provider> ProvidersList { get; set; }
        public List<FuelTypes> FuelTypesList { get; set; }

        [JsonProperty("fuel_units")]
        public List<string> FuelUnits { get; set; }
        [JsonProperty("limit_type")]
        public List<string> LimitTypes { get; set; }
        [JsonProperty("country")]
        public List<string> Country { get; set; }
        [JsonProperty("refuelling_stations")]
        public List<string> RefuilimgStations { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
