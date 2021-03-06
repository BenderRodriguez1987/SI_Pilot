﻿using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class DeviceRegistration
    {
        [AliasAs("login")]
        public string Login { get; set; }

        [AliasAs("shared")]
        public string Shared { get; set; }

        [AliasAs("shared_id")]
        public string SharedId { get; set; }

        [AliasAs("device_id")]
        public string DeviceId { get; set; }

        [AliasAs("device_model")]
        public string DeviceModel { get; set; }

        [AliasAs("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
