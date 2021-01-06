using Newtonsoft.Json;
using SI_Master.Managers.SendLimitsManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SendLimitsManager))]
namespace SI_Master.Managers.SendLimitsManager
{
    public class SendLimitsManager: ISendLimitsManager
    {
        IAuthManager authmanager = DependencyService.Get<IAuthManager>();
        INetworkService networkservice = DependencyService.Get<INetworkService>();

        public async Task LockOrUnlockCard(Dictionary<string, Dictionary<string, string>> cardBlock) {
            try {
                Dictionary<string, string> sendDictionary = new Dictionary<string, string>();
                string cardBlockString = JsonConvert.SerializeObject(cardBlock);
                sendDictionary.Add("block", cardBlockString);
                await networkservice.LockOrUnlockCard(sendDictionary, authmanager.GetAuthData());
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }


        public async Task SendCardLimits(SendCardLimits CardLimits) {
            try {
                Dictionary<string, string> sendingDic = new Dictionary<string, string>();
                string limitStr = JsonConvert.SerializeObject(CardLimits.Limits);
                sendingDic.Add("limits", limitStr);
                sendingDic.Add("date", CardLimits.Date);
                sendingDic.Add("country", CardLimits.Country);
                sendingDic.Add("refuelling_stations", CardLimits.RefuelingStations);
                await networkservice.SendCardLimits(sendingDic, authmanager.GetAuthData());
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
