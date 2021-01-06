using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Common;
using SI_Master.Exceptions;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DashboardManager))]
namespace SI_Master.Managers
{
    public class DashboardManager : IDashboardManager
    {

        INetworkService networkService = DependencyService.Get<INetworkService>();
        IAuthManager authManager = DependencyService.Get<IAuthManager>();

        private AboutOrganozation aOrganization;

        private Dashboard dashboard;
        public async Task<AboutOrganozation> GetDashboard()
        {
            Answer answer = new Answer();
            AboutOrganozation dashboard = new AboutOrganozation();
            try
            {
                 answer = await networkService.NetworkRequest(NetworkService.TaskType.Getdashboard, authManager.GetAuthData(), null);
                if (answer != null && answer.ResData is JObject data )
                {
                    if(data.TryGetValue("institution", out JToken aboutOrganization))
                    {
                        aOrganization = JsonConvert.DeserializeObject<AboutOrganozation>(aboutOrganization.ToString());
                    }
                    dashboard = aOrganization;

                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return dashboard;
        }

        public async Task<List<Contracts>> GetDesktop()
        {
            Answer answer = new Answer();
            List<Contracts> contracts = new List<Contracts>();
            try
            {
                answer = await networkService.NetworkRequest(NetworkService.TaskType.Getdashboard, authManager.GetAuthData(), null);
                if (answer != null && answer.ResData is JObject data)
                {
                    if (data.TryGetValue("contracts", out  JToken contract))
                    {
                        if (contract is JObject Jcontracts) 
                        {
                            if(Jcontracts.TryGetValue("providers", out JToken Jporviders))
                            {
                                if(Jporviders is JArray providers)
                                {
                                    contracts = JsonConvert.DeserializeObject<List<Contracts>>(providers.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return contracts;
        }

        public async Task<long> GetMobileCode()
        {
            _ = new Answer();
            Answer answer;
            long mobileCde = 0;
            try
            {
                answer = await networkService.NetworkRequest(NetworkService.TaskType.MobileAppCode, authManager.GetAuthData(), null);
                if(answer != null && answer.ResData is JObject data)
                {
                    if (data.TryGetValue("code", out JToken jcode)) {
                        mobileCde = JsonConvert.DeserializeObject<long>(jcode.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return mobileCde;
        }

        public async Task OrderKeys(int keyAmount)
        {
            Answer answerr = new Answer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("amount", keyAmount);
            try
            {
                answerr = await networkService.NetworkRequest(NetworkService.TaskType.OrderKeys, authManager.GetAuthData(), dic);
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
