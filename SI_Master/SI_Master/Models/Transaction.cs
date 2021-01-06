using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using Newtonsoft.Json;

namespace SI_Master.Models
{
    public class Transaction
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("refuelling_station")]
        public string RefuelingStation { get; set; }

        [JsonProperty("refuelling_station_address")]
        public string RefuelingStationAdress { get; set; }

        [JsonProperty("fuel_name")]
        public string FuelName { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("card_holder")]
        public string CardHolder { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("fuel_amount")]
        public float FuelAmount { get; set; }

        [JsonProperty("real_fuel_amount")]
        public float RealFuelAmount { get; set; }

        [JsonProperty("fuel_units")]
        public string FuelUnits { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("base_price")]
        public float BasePrice { get; set; }

        [JsonProperty("discount")]
        public float Discount { get; set; }

        [JsonProperty("client_price")]
        public float ClientPrice { get; set; }

        [JsonProperty("sum")]
        public float Sum { get; set; }

        [JsonProperty("real_sum")]
        public float RealSum { get; set; }

        public string ForMatedDateStr
        {
            get
            {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }

        public string FuelAmountAndUnit => FuelAmount + " " + FuelUnits;
        public string ClientPriceWithRubles => ClientPrice + " р. за " + FuelUnits;
        public string RealSumWithRubles => RealSum + " р.";

        public bool IsNotReward {
            get {
                if (FuelName == "Вознаграждение") {
                    return false;
                } else {
                    return true;
                }
            }
        }
        public string CardNumberStr
        {
            get
            {
                if (FuelName == "Вознаграждение")
                {
                    return FuelName;
                } else
                {
                    return CardNumber;
                }
            }
        }
    }
}
