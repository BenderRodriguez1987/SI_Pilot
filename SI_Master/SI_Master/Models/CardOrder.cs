using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SI_Master.Managers.StatusColorHandler;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class CardOrder
    {
        [JsonProperty("limits")]
        public List<Limits> Limits { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("refuelling_stations")]
        public string Station { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        public string ForMatedDateStr
        {
            get
            {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }

        IStatusColorHandler colorHandler = DependencyService.Get<IStatusColorHandler>();
        public System.Drawing.Color StatusColor => colorHandler.GetColorFromStatus(Status);
    }
}
