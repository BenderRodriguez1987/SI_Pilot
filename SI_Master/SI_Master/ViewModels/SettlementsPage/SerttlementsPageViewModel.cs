using SI_Master.Managers.SettlementsManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.SettlementsPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SerttlementsPageViewModel))]
namespace SI_Master.ViewModels.SettlementsPage
{
    public class SerttlementsPageViewModel : BaseViewModel, ISerttlementsPageViewModel
    {

        ISettlementsManager settlementsmanager = DependencyService.Get<ISettlementsManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        private int UserIdforCurrentSource { get; set; } = 0;

        public string Title { get; set; } = "Взаиморасчёты";

        private Totals _totals { get; set; }
        public Totals Totals {
            get {
                return _totals;
            } set {
                _totals = value;
                OnPropertyChanged("Totals");
            }
        }

        private Total2 _total2 {get;set;}
        public Total2 Total2 {
            get {
                return _total2;
            } set {
                _total2 = value;
                OnPropertyChanged("Total2");
            }
        }
        private List<Settlement> _itemsource { get; set; }
        public List<Settlement> ItemSource
        { get {
                return _itemsource;
            } set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
            }
        }
        private float _debt { get; set; }
        public float Debt { get {
                return _debt;
            } set {
                _debt = value;
                OnPropertyChanged("Debt");
            } }

        private Color _balanceColor { get; set; }
        private string _debtStr { get; set; }
        public string DebtLabel {
            get
            {
                return _debtStr;
            }
            set
            {
                _debtStr = value;
                OnPropertyChanged("DebtLabel");
            }
            }

        public async Task LoadSettlements(Dictionary<string, object> _filterParams)
        {
            //ItemSource = await settlementsmanager.GetSettlements();
            if(ItemSource == null && Totals == null || UserIdforCurrentSource != authSettings.Active)
            {
                var items = await settlementsmanager.GetSettlementsWithTotals(null);
                ItemSource = items.SettlementsList;
                Totals = items.Totals;
                Total2 = items?.Total2;
                Debt = items.Total2.Sum - items.Totals.SumOut;
                if (Debt > 0)
                {
                    DebtLabel = "Задолженность за " + ((UserAuthData)authSettings.ActiveUser()).Caption;
                }
                else
                {
                    DebtLabel = "Задолженность за " + "ООО АСМАП-сервис";
                }
                UserIdforCurrentSource = authSettings.Active;

            }
            if(_filterParams != null)
            {
                var items = await settlementsmanager.GetSettlementsWithTotals(_filterParams);
                ItemSource = items.SettlementsList;
                Totals = items.Totals;
                Total2 = items?.Total2;
                Debt = items.Total2.Sum - items.Totals.SumOut;
                if(Debt > 0) {
                    DebtLabel = "Задолженность за " + ((UserAuthData)authSettings.ActiveUser()).Caption;
                } else
                {
                    DebtLabel = "Задолженность за " + "ООО АСМАП-сервис";
                }
            }
        }
    }
}
