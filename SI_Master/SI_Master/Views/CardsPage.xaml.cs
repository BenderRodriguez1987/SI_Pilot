using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using SI_Master.Common;
using SI_Master.Models;
using SI_Master.PopUps;
using SI_Master.ViewModels.CardsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {

        ICardsPageViewModel _viewModel = DependencyService.Get<ICardsPageViewModel>();

        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();
        public ICommand LimitsPopupShowClicked { get; set; }
        public CardsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
            LimitsPopupShowClicked = new Command(execute: (object arg) => { if (arg is Card) LimitsPopupShow((Card)arg); });
            //CardsListView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            //{
            //    if(CardsListView.SelectedItem != null)
            //    {
            //        var limits = (Card)e.SelectedItem;
            //        var cardLimitsPopup = new CardLimitsPopup(limits.CardLimits, !limits.IsBlock, false)
            //        {
            //            ChangeLimitsClicked = async () =>
            //            {
            //                DictionaryLimits dic = await _viewModel.GetLimitsFromProvider(limits.Supplier);
            //                List<Limits> li = limits.CardLimits;
            //                _ = Navigation.PushAsync(new ChangeLimitsPage(dic, li, limits.Country, limits.Stations, limits.Number));
            //            },
            //            BlockCard = () =>
            //            {
            //                OnDelete(limits);
            //            }
            //        };
            //        CardsListView.SelectedItem = null;
            //        await PopupNavigation.Instance.PushAsync(cardLimitsPopup);
            //    }
            //};
        }

        public async void LimitsPopupShow(Card card)
        {
            {
                var cardLimitsPopup = new CardLimitsPopup(card.CardLimits, !card.IsBlock, false)
                {
                    ChangeLimitsClicked = async () =>
                    {
                        DictionaryLimits dic = await _viewModel.GetLimitsFromProvider(card.Supplier);
                        List<Limits> li = card.CardLimits;
                        _ = Navigation.PushAsync(new ChangeLimitsPage(dic, li, card.Country, card.Stations, card.Number));
                    },
                    BlockCard = () =>
                    {
                        OnDelete(card);
                    }
                };
                CardsListView.SelectedItem = null;
                await PopupNavigation.Instance.PushAsync(cardLimitsPopup);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
                UserDialogs.Instance.ShowLoading();
                await _viewModel.LoadCards(null);
                UserDialogs.Instance.HideLoading();
        }

        public async void ApplyFilters()
        {
            await _viewModel.LoadCards(_filterParams);
        }
        public async void OnMore(object sender, EventArgs e)
        {

            var mi = (MenuItem)sender;
            var sc = (Card)mi.CommandParameter;
            if( sc.IsBlock)
            {
                var config = new AlertConfig();
                config.Message = "Карта заблокирована, изменение лимитов невозможно";
                config.OkText = "OK";

                UserDialogs.Instance.Alert(config);
            } else {
                DictionaryLimits dic = await _viewModel.GetLimitsFromProvider(sc.Supplier);
                List<Limits> li = sc.CardLimits;
                _ = Navigation.PushAsync(new ChangeLimitsPage(dic, li, sc.Country, sc.Stations, sc.Number));
            }
        }

        public void OnDelete(Card limits)
        {
            if (limits.IsBlock) {
                var cardBlockPopup = new CardBlockPopup(CardBlockPopup.CardAction.Unlock, limits.Number);
                PopupNavigation.Instance.PushAsync(cardBlockPopup);
            } else {
                var cardBlockPopup = new CardBlockPopup(CardBlockPopup.CardAction.Block, limits.Number);
                PopupNavigation.Instance.PushAsync(cardBlockPopup);
            }
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            CardsListView.ItemsSource = _viewModel.ItemSource.FindAll(i => i.Caption.Contains(e.NewTextValue) || i.Supplier.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.Cards, _filterParams, ApplyFilters));
        }

    }
}