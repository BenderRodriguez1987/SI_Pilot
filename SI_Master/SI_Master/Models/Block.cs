using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
using Refit;
using SI_Master.Managers.StatusColorHandler;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class Block
    {
        IStatusColorHandler colorHandler = DependencyService.Get<IStatusColorHandler>();

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        public string ForMatedDateStr
        {
            get {
                return Date.Replace("-", ".") + " (МСК)";
            }
        }

        public System.Drawing.Color StatusColor => colorHandler.GetColorFromStatus(Status);

    }
}
