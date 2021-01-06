using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Managers.ArchiveCardOrderManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ArchiveCradOrderManager))]
namespace SI_Master.Managers.ArchiveCardOrderManager
{
    public class ArchiveCradOrderManager : IArchiveCradOrderManager
    {

        IAuthManager authmanager = DependencyService.Get<IAuthManager>();
        INetworkService networkservice = DependencyService.Get<INetworkService>();
        public async Task<List<CardOrder>> GetArchiveCardOrders(Dictionary<string, object> _filterParams)
        {
            Answer answer = new Answer();
            List<CardOrder> ArchiveCardOrderList = new List<CardOrder>();
            Dictionary<string, object> _tmpFilter = new Dictionary<string, object>();
            try
            {
                if (_filterParams != null)
                {
                    _tmpFilter = _filterParams.ToDictionary(entry => entry.Key, entry => entry.Value);
                    if (_tmpFilter.TryGetValue("filters[date_from]", out object dt) && dt is DateTime)
                    {
                        _tmpFilter["filters[date_from]"] = string.Format("{0:yyyy-MM-dd}", dt);
                    }
                    if (_tmpFilter.TryGetValue("filters[date_to]", out object dt_to) && dt_to is DateTime)
                    {
                        _tmpFilter["filters[date_to]"] = string.Format("{0:yyyy-MM-dd}", dt_to);
                    }
                }
                answer = await networkservice.NetworkRequest(NetworkService.TaskType.ArchiveCards, authmanager.GetAuthData(), _tmpFilter);
                if (answer != null && answer.ResData is JObject jData)
                {
                    if (jData.TryGetValue("items", out JToken jArray))
                    {
                        ArchiveCardOrderList = JsonConvert.DeserializeObject<List<CardOrder>>(jArray.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return ArchiveCardOrderList;
        }
    }
}
