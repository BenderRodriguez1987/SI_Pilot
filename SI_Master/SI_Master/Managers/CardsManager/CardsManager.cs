using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Managers.CardsManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(CardsManager))]
namespace SI_Master.Managers.CardsManager
{
    public class CardsManager : ICardsManager
    {
        IAuthManager authmanager = DependencyService.Get<IAuthManager>();
        INetworkService networkservice = DependencyService.Get<INetworkService>();

        public async Task<List<Card>> GetCrds(Dictionary<string, object> _filterParams)
        {
            Answer answer = new Answer();
            List<Card> CardsList = new List<Card>();
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
                answer = await networkservice.NetworkRequest(NetworkService.TaskType.Cards, authmanager.GetAuthData(), _tmpFilter);
                if (answer != null && answer.ResData is JObject jData)
                {
                    if (jData.TryGetValue("items", out JToken jArray))
                    {
                        CardsList = JsonConvert.DeserializeObject<List<Card>>(jArray.ToString());
                    }
                }
            } catch(Exception e )
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return CardsList;
        }

        public async Task<DictionaryLimits> GetLimitsFromProvider(string provider)
        {
            DictionaryLimits dictionaryLimits = await GetDictionaryLimits();
            var providerslimits = dictionaryLimits.ProvidersList.Where(pr => pr.Title == provider);
            dictionaryLimits.ProvidersList = (List<Provider>)providerslimits;
            return dictionaryLimits;
        }


        public async Task<DictionaryLimits> GetDictionaryLimits()
        {
            Answer answer = new Answer();
            List<Provider> ProvidersList = new List<Provider>();
            List<FuelTypes> FuelTypesList = new List<FuelTypes>();
            DictionaryLimits dictionaryLimits = new DictionaryLimits();
            Dictionary<string, List<string>> ProvidersDictionary;
            Dictionary<string, List<string>> FuelTypesDictionary;
            try
            {
                answer = await networkservice.NetworkRequest(NetworkService.TaskType.DictionaryLimits, authmanager.GetAuthData(), null);
                if (answer != null && answer.ResData is JObject jData)
                {
                    dictionaryLimits = JsonConvert.DeserializeObject<DictionaryLimits>(jData.ToString());
                    if (jData.TryGetValue("providers", out JToken jToken))
                    {
                        ProvidersDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jToken.ToString());
                        if (ProvidersDictionary != null && ProvidersDictionary.Count > 0)
                        {
                            foreach (KeyValuePair<string, List<string>> entry in ProvidersDictionary)
                            {
                                Provider provider = new Provider();
                                provider.Title = entry.Key;
                                provider.ListProviders = entry.Value;
                                ProvidersList.Add(provider);
                            }
                        }
                    }
                    if (jData.TryGetValue("fuel_type", out JToken jTokenFuel))
                    {
                        FuelTypesDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jTokenFuel.ToString());
                        if (FuelTypesDictionary != null && FuelTypesDictionary.Count > 0)
                        {
                            foreach (KeyValuePair<string, List<string>> entry in FuelTypesDictionary)
                            {
                                FuelTypes fuelType = new FuelTypes();
                                fuelType.Title = entry.Key;
                                fuelType.ListFuelTypes = entry.Value;
                                FuelTypesList.Add(fuelType);
                            }
                        }
                    }

                    dictionaryLimits.ProvidersList = ProvidersList;
                    dictionaryLimits.FuelTypesList = FuelTypesList;
                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return dictionaryLimits;
        }

        //public async Task<List<Provider>> ProvidersList()
        //{
        //    Answer answer = new Answer();
        //    List<Provider> ProvidersList = new List<Provider>();
        //    Dictionary<string, List<string>> ProvidersDictionary;
        //    try
        //    {
        //        answer = await networkservice.NetworkRequest(NetworkService.TaskType.DictionaryLimits, authmanager.GetAuthData());
        //        if (answer != null && answer.ResData is JObject jData)
        //        {
        //            if (jData.TryGetValue("providers", out JToken jToken))
        //            {
        //                ProvidersDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jToken.ToString());
        //                if (ProvidersDictionary != null && ProvidersDictionary.Count > 0)
        //                {
        //                    foreach (KeyValuePair<string, List<string>> entry in ProvidersDictionary)
        //                    {
        //                        Provider provider = new Provider();
        //                        provider.Title = entry.Key;
        //                        provider.ListProviders = entry.Value;
        //                        ProvidersList.Add(provider);
        //                    }
        //                }
        //            }
        //        }

        //    } catch(Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e);
        //    }

        //    return ProvidersList;
        //}

        //public async Task<List<FuelTypes>> FuelTypesList()
        //{
        //    Answer answer = new Answer();
        //    List<FuelTypes> FuelTypesList = new List<FuelTypes>();
        //    Dictionary<string, List<string>> FuelTypesDictionary;
        //    try
        //    {
        //        answer = await networkservice.NetworkRequest(NetworkService.TaskType.DictionaryLimits, authmanager.GetAuthData());
        //        if (answer != null && answer.ResData is JObject jData)
        //        {
        //            if (jData.TryGetValue("fuel_type", out JToken jToken))
        //            {
        //                FuelTypesDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jToken.ToString());
        //                if (FuelTypesDictionary != null && FuelTypesDictionary.Count > 0)
        //                {
        //                    foreach (KeyValuePair<string, List<string>> entry in FuelTypesDictionary)
        //                    {
        //                        FuelTypes fuelType = new FuelTypes();
        //                        fuelType.Title = entry.Key;
        //                        fuelType.ListFuelTypes = entry.Value;
        //                        FuelTypesList.Add(fuelType);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e);
        //    }

        //    return FuelTypesList;
        //}

        //public async Task<List<LimitTypes>> LimitTypesList()
        //{
        //    Answer answer = new Answer();
        //    List<LimitTypes> LimitTypesList = new List<LimitTypes>();
        //    Dictionary<string, List<string>> LimitTypesDictionary;
        //    try
        //    {
        //        answer = await networkservice.NetworkRequest(NetworkService.TaskType.DictionaryLimits, authmanager.GetAuthData());
        //        if (answer != null && answer.ResData is JObject jData)
        //        {
        //            if (jData.TryGetValue("limit_type", out JToken jToken))
        //            {
        //                LimitTypesDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jToken.ToString());
        //                if (LimitTypesDictionary != null && LimitTypesDictionary.Count > 0)
        //                {
        //                    foreach (KeyValuePair<string, List<string>> entry in LimitTypesDictionary)
        //                    {
        //                        LimitTypes LimitType = new LimitTypes();
        //                        LimitType.Title = entry.Key;
        //                        LimitType.LimitTypesList = entry.Value;
        //                        LimitTypesList.Add(LimitType);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e);
        //    }

        //    return LimitTypesList;
        //}
    }
}
