using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class QRObject
    {
        [JsonProperty("accepted")]
        public bool Acceted { get; set; }

        [JsonProperty("visit_id")]
        public int Visit_id { get; set; }

        [JsonProperty("with_qr")]
        public bool With_qr { get; set; }

        [JsonProperty("qr")]
        public string Qr { get; set; }
    }
}
