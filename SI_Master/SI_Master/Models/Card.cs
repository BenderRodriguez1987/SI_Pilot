using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Card
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("holder")]
        public string Holder { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("contract")]
        public string Contract { get; set; }

        [JsonProperty("limits")]
        public List<Limits> CardLimits { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("refuelling_stations")]
        public string Stations { get; set; }

        public string StatusText { get; set; }

        public bool IsBlock
        {
            get
            {
                return !Active;
            } set
            {
                IsBlock = !value;
            }
        }

        public string Update { get {
                return Stations + " " + Country;
            } set { Update = value; } }

        public string Caption { get { return string.Format("{0}\n{1}", Number, Contract); } }

        public Color BalanceColor { get; set; }
        public Color StatusColor { get {
                if (Active)
                {
                    StatusText = "Активная";
                    return Color.Green;
                } else
                {
                    StatusText = "Заблокирована";
                    return Color.Red;
                }
            } set { StatusColor = value; } }

        public string BalanceText { get {
                return Supplier;
            } set { BalanceText = value; } }
        public string LimitsButtonText { get; set; } = "\ue685";
    }
}
