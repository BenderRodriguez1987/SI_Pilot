using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class NegotiatedOrder
    {
        [JsonProperty("wheels")]
        public List<Wheel> Wheels { get; set; }

        [JsonProperty("add_works")]
        public List<AdditionalWork> AdditionalWorks { get; set; }
    }
}
