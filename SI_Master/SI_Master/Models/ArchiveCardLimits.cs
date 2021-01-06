using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SI_Master.Managers.StatusColorHandler;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class ArchiveCardLimits
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("limits")]
        public List<Limits> Limits { get; set; }

        [JsonProperty("date_to_set")]
        public string DateToSet { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("refuelling_stations")]
        public string RefuelingStations { get; set; }

        public string ForMatedDateStr
        {
            get
            {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }

        IStatusColorHandler colorHandler = DependencyService.Get<IStatusColorHandler>();
        public System.Drawing.Color StatusColor => colorHandler.GetColorFromStatus(Status);

        public string LimitsButtonText { get; set; } = "\ue685";

    }
}
