using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Totals
    {
        [JsonProperty("sum_in")]
        public float SumIn { get; set; }

       [JsonProperty("sum_out")]
       public float SumOut { get; set; }

        [JsonProperty("turn_bill")]
        public float TurnBill { get; set; }

        [JsonProperty("turn_pay")]
        public float TurnPay { get; set; }

    }
}
