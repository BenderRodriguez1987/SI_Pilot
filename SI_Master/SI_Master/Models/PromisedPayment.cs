using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Refit;

namespace SI_Master.Models
{
    public class PromisedPayment
    {
        public float Arround { get; set; }
        public string PaymentOrder { get; set; }
        public string Supplier { get; set; }
    }
}
