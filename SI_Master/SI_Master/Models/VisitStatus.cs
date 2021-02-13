using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public  class VisitStatus
    {
        [JsonProperty("status")]
        public int Status { get; set; }

    }
}
