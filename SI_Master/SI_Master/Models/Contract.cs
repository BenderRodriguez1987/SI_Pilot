using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class Contract
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("start_date")]
        public DateTime Date { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("block")]
        public bool Block { get; set; }

        public string Caption { get { return string.Format("{0}\n{1}", Provider, Number); } }
        public double BalanceText { get; set; }
        public Color BalanceColor  {
            get  {
                if (BalanceText <= 0)  {
                    return Color.Red;
                }
                else   {
                    return Color.Black;
                }
            }
            set   {
                BalanceColor = value;
            }
        }
 
        public string StatusText { get; set; }
        public Color StatusColor { get
            {
                if (Block)
                {
                    StatusText = "Заблокировано";
                    return Color.Red;
                }
                else
                {
                    StatusText = "Активный";
                    return Color.Green;
                }
            } set { StatusColor = value; } }
        public string Update { get; set; }

        public Contract() {
            Update = Date.ToString();
        }
    }
}
