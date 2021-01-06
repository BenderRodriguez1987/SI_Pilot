using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI_Master.Managers.SendLimitsManager;
using SI_Master.Models;
using SI_Master.ViewModels.ChangeLimitsPage;
using Xamarin.Forms;

[assembly: Dependency(typeof(ChangeLimitsPageViewModel))]
namespace SI_Master.ViewModels.ChangeLimitsPage
{
    public class ChangeLimitsPageViewModel : BaseViewModel, IChangeLimitsPageViewModel
    {
        ISendLimitsManager cardManager = DependencyService.Get<ISendLimitsManager>();
        string Title { get; set; } = "Изменение лимитов";
        private List<LimitsItem> _DataSource { get; set; }
        public List<LimitsItem> DataSource { get {
                return _DataSource;
            } set {
                _DataSource = value;
                OnPropertyChanged("DataSource");
            } }

        public void SetDictionary(DictionaryLimits dictionaryLimits, List<Limits> limitsList)
        {
                DataSource = new List<LimitsItem>();
            foreach (var item in dictionaryLimits.ProvidersList)
            {
                foreach(var pr in item.ListProviders)
                {
                    LimitsItem lI = new LimitsItem();
                    var tmplist = dictionaryLimits.FuelTypesList.Where(ft => ft.Title == pr);
                    var currentLimit = limitsList.Find(l => l.FuelType == pr);
                    List<string> _tmplist = new List<string>();
                    foreach(var t in tmplist)
                    {
                        _tmplist.AddRange(t.ListFuelTypes);
                    }
                    lI.LimitsName = pr;
                    lI.FuelTypesList = _tmplist;
                    lI.LimitTypes = dictionaryLimits.LimitTypes;
                    lI.RefuilimgStations = dictionaryLimits.RefuilimgStations;
                    lI.Country = dictionaryLimits.Country;
                    if (currentLimit != null)
                    {
                        lI.FuelAmount = currentLimit.FuelAmount;
                        lI.CurrentFuelType = currentLimit.FuelUnits;
                        lI.CurrentlimitType = currentLimit.LimitType;
                    }
                    DataSource.Add(lI);
                }
            }
        }

        public async Task SendCardLimits(SendCardLimits sendCardLimits)
        {
            await cardManager.SendCardLimits(sendCardLimits);
        }
    }
}
