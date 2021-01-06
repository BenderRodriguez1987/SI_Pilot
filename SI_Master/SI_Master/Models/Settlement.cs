using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Settlement
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("contract")]
        public string Contract { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("sum_in")]
        public float SumIn { get; set; }

        [JsonProperty("sum_out")]
        public float SumOut { get; set; }

        [JsonProperty("flow")]
        public float Flow { get; set; }

        public string ForMatedDateStr
        {
            get
            {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }
        public Color BalanceColor
        {
            get
            {
                if (Flow >= 0)
                {
                    return Color.Red;
                }
                else
                {
                    return Color.Green;
                }
            }
            set { BalanceColor = value; }
        }
        public string Sum
        {
            get
            {
                float total = SumIn - SumOut;
                return total.ToString();
            }
        }
    }
}
