using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class AboutOrganozation
    {
        [JsonProperty("address")]
        public string Adress { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [JsonProperty("ogrn")]
        public string Ogrn { get; set; }

        [JsonProperty("full_caption")]
        public string FullCaption { get; set; }

    }
}
