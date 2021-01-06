using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models {
    public class FCMAuthData {
        [AliasAs("client[]")]
        public string Client { get; set; }

        [AliasAs("token[]")]
        public string Token { get; set; }

        [AliasAs("shared")]
        public string Shared { get; set; }

        [AliasAs("shared_id")]
        public string SharedId { get; set; }

        [AliasAs("device_id")]
        public string DeviceId { get; set; }
        [AliasAs("fcm_token")]
        public string FCMToken { get; set; }
    }
}
