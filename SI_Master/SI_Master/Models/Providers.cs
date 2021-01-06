using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Providers
    {
        [JsonProperty("providers")]
        public List<Contracts> Contracts { get; set; }
    }
}
