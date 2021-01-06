using SI_Master.Managers.CardsManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.CardsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardsPageViewModel))]
namespace SI_Master.ViewModels.CardsPage
{
    public class CardsPageViewModel : BaseViewModel, ICardsPageViewModel
    {

        ICardsManager cardManager = DependencyService.Get<ICardsManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        private int UserIdforCurrentSource { get; set; } = 0;
        public string Title { get; set; } = "Топливные карты";
        private List<Card> _itemsource { get; set; }
        public List<Card> ItemSource { get {
                return _itemsource;
            } set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
            } }

        private List<Provider> _providersList { get; set; }
        public List<Provider> ProvidersList { get {
                return _providersList;
            } set {
                _providersList = value;
                OnPropertyChanged("ProvidersList");
            } }

        private List<FuelTypes> _FuelTypeList
        {            get; set;        }
        public List<FuelTypes> FuelTypeList
        {
            get
            {
                return _FuelTypeList;
            }
            set
            {
                _FuelTypeList = value;
                OnPropertyChanged("FuelTypeList");
            }
        }

        private DictionaryLimits _dictionaryLimits { get; set; }
        public DictionaryLimits dictionaryLimits
        {
            get
            {
                return _dictionaryLimits;
            } set
            {
                _dictionaryLimits = value;
                OnPropertyChanged("dictionaryLimits");
            }
        }
        public CardsPageViewModel()
        {
            ////mok
            //ItemSource = new List<Card>();
            //for (int i =0; i < 5; i++)
            //{
            //    ItemSource.Add(new Card
            //    {
            //        Number = "12341234123",
            //        Holder = "Слонопотам",
            //        Active = true,
            //        Contract = "666",
            //        Country = "Zimbabve",
            //        Supplier = "Petronas",
            //        Stations = "Везде"
            //    });
            //}
        }

        public async Task LoadCards(Dictionary<string, object> _filterParams)
        {
            if (ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await cardManager.GetCrds(null);
                UserIdforCurrentSource = authSettings.Active;
            }
            if(_filterParams != null)
            {
                ItemSource = await cardManager.GetCrds(_filterParams);
            }
            if (dictionaryLimits == null)
            {
                dictionaryLimits = await cardManager.GetDictionaryLimits();
            }
        }

        public async Task<DictionaryLimits> GetLimitsFromProvider(string provider)
        {
            DictionaryLimits tmpLimits = new DictionaryLimits();
            if (dictionaryLimits != null || dictionaryLimits.ProvidersList.Count > 0)
            {
                tmpLimits = (DictionaryLimits)dictionaryLimits.Clone();
                var providerslimits = tmpLimits.ProvidersList.FindAll(pr => pr.Title == provider);
                tmpLimits.ProvidersList = (List<Provider>)providerslimits;
                return tmpLimits;
            } else
            {
                await LoadCards(null);
                var providerslimits = dictionaryLimits.ProvidersList.FindAll(pr => pr.Title == provider);
                tmpLimits.ProvidersList = (List<Provider>)providerslimits;
                return tmpLimits;
            }
        }
    }
}
