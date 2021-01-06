using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class LimitsDictionary
    {
        [JsonProperty("providers")]
        public List<Provider> ProvidersList { get; set; }

    }
}
