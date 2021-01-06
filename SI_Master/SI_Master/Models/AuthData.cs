using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Refit;

namespace SI_Master.Models
{
    public class AuthData
    {
        [AliasAs("client")]
        public string Client { get; set; }

        [AliasAs("token")]
        public string Token { get; set; }

        [AliasAs("shared")]
        public string Shared { get; set; }

        [AliasAs("shared_id")]
        public string SharedId { get; set; }

        [AliasAs("device_id")]
        public string DeviceId { get; set; }
    }
}
