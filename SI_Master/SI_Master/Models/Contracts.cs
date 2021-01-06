using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class Contracts
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("blocked")]
        public bool Blocked { get; set; }

        [JsonProperty("sum")]
        public float Sum { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        public string ForMatedDateStr
        {
            get
            {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }

        public string Caption { get { return string.Format("{0}\n{1}", Provider, Number); } }
        public string StatusText
        {
            get {
                if (Blocked)  {
                    return "Заблокирована";
                } else {
                    return "Активна";
                }
            }
        }

        public Color BalanceColor
        {
            get
            {
                if (Sum <= 0)
                {
                    return Color.Red;
                }
                else
                {
                    return Color.Green;
                }
            }
            set
            {
                BalanceColor = value;
            }
        }
        public Color StatusColor
        {
            get
            {
                if (Blocked) {
                    return (Color)Application.Current.Resources["NegariveBalanceBackgroundColor"];
                }
                else {
                    return (Color)Application.Current.Resources["PositiveBalanceBackgroundColor"];
                }
            }
            set { StatusColor = value; }
        }
    }
}
