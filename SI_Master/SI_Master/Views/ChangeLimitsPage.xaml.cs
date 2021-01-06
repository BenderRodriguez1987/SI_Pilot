using Rg.Plugins.Popup.Services;
using SI_Master.Models;
using SI_Master.PopUps;
using SI_Master.ViewModels.ChangeLimitsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeLimitsPage : ContentPage
    {

        IChangeLimitsPageViewModel viewModel = DependencyService.Get<IChangeLimitsPageViewModel>();
        private string Number { get; set; }
        private string Stations { get; set; }
        private string _Country { get; set; }
        public ChangeLimitsPage(DictionaryLimits dictionaryLimits, List<Limits> li, string Country, string RefuelStations, string cardNumber) {
            InitializeComponent();
            viewModel.SetDictionary(dictionaryLimits, li);
            Number = cardNumber;
            Stations = RefuelStations;
            _Country = Country;
            BindingContext = viewModel;

            var _saveLimitsButton = new ToolbarItem() {
                IconImageSource = "ic_ok"
            };
            
            ToolbarItems.Add(_saveLimitsButton);
            _saveLimitsButton.Clicked += SaveButtonClicked;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            List<Limits> list = new List<Limits>();
            foreach(LimitsItem item in viewModel.DataSource) {
                Limits limit = new Limits();
                {
                    if(item.LimitsName != null && item.CurrentFuelType != null && item.CurrentlimitType != null) {
                        limit.FuelType = item.LimitsName;
                        limit.FuelAmount = item.FuelAmount;
                        limit.FuelUnits = item.CurrentFuelType;
                        limit.LimitType = item.CurrentlimitType;
                        list.Add(limit);
                    }
                };
            }
            var checkLimitsPopup = new CardLimitsPopup(list, false, false) {
                OkClicked = () => {
                    //TODO проверить лимиты на валидность и отправить запрос на сервер с лимитами
                    SendCardLimits cardLimits = new SendCardLimits();
                    Dictionary<string, List<Limits>> limitsDic = new Dictionary<string, List<Limits>>();
                    limitsDic.Add(Number, list);
                    cardLimits.Limits = limitsDic;
                    cardLimits.Date = DateTime.Now.ToString("yyyy-MM-dd");
                    cardLimits.Country = _Country;
                    cardLimits.RefuelingStations = Stations;
                    viewModel.SendCardLimits(cardLimits);
                }
            };
            await PopupNavigation.Instance.PushAsync(checkLimitsPopup);
        }
    }
}