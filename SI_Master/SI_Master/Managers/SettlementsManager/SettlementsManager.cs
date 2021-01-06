using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Managers.SettlementsManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettlementsManager))]
namespace SI_Master.Managers.SettlementsManager
{
    public class SettlementsManager: ISettlementsManager
    {

        IAuthManager authmanager = DependencyService.Get<IAuthManager>();
        INetworkService networkservice = DependencyService.Get<INetworkService>();
        public async Task<List<Settlement>> GetSettlements()
        {
            Answer answer = new Answer();
            List<Settlement> SettlementsList = new List<Settlement>();
            try
            {
                answer = await networkservice.NetworkRequest(NetworkService.TaskType.Settlements, authmanager.GetAuthData(), null);
                if (answer != null && answer.ResData is JObject jData)
                {
                    if (jData.TryGetValue("items", out JToken jArray))
                    {
                        SettlementsList = JsonConvert.DeserializeObject<List<Settlement>>(jArray.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return SettlementsList;
        }

        public async Task<Settlements> GetSettlementsWithTotals(Dictionary<string, object> _filterParams)
        {
            Answer answer = new Answer();
            Settlements settlements = new Settlements();
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
                answer = await networkservice.NetworkRequest(NetworkService.TaskType.Settlements, authmanager.GetAuthData(), _tmpFilter);
                if (answer != null && answer.ResData is JObject jData)
                {
                    settlements = JsonConvert.DeserializeObject<Settlements>(jData.ToString());
                }
            } catch( Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return settlements;
        }
    }
}
