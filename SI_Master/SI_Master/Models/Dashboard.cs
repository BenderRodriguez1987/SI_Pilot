using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Dashboard
    {
        [JsonProperty("institution")]
        public AboutOrganozation AboutOrganozation  { get; set;}

        [JsonProperty("contracts")]
        public Providers Providers { get; set; }
    }
}
