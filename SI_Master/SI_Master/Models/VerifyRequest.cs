using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class VerifyRequest
    {
        [AliasAs("device_model")]
        public string DeviceModel { get; set; }

        [AliasAs("login")]
        public string Login { get; set; }

        [AliasAs("code")]
        public string Code { get; set; }

        [AliasAs("phone_number")]
        public string Phone { get; set; }

        [AliasAs("shared")]
        public string Shared { get; set; }

        [AliasAs("shared_id")]
        public string SharedId { get; set; }

        [AliasAs("device_id")]
        public string DeviceId { get; set; }
    }
}
