using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class VerifyResponse
    {

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
