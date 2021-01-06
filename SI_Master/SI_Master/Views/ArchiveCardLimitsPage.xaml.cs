using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using SI_Master.Common;
using SI_Master.Models;
using SI_Master.PopUps;
using SI_Master.ViewModels.ArchiveCardsLimitsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchiveCardLimitsPage : ContentPage
    {
        IArchiveCardsLimitsPageViewModel viewModel = DependencyService.Get<IArchiveCardsLimitsPageViewModel>();
        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();
        public ICommand LimitsPopupShowClicked { get; set; }
        public ArchiveCardLimitsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            LimitsPopupShowClicked = new Command(execute: (object arg) => { if (arg is ArchiveCardLimits) LimitsPopupShow((ArchiveCardLimits)arg); });
            //ArchiveCardsListView.ItemSelected += async  (object sender, SelectedItemChangedEventArgs e) =>
            //{
            //    if (ArchiveCardsListView.SelectedItem != null)
            //    {
            //        var limits = (ArchiveCardLimits)e.SelectedItem;
            //        var cardLimitsPopup = new CardLimitsPopup(limits.Limits, false, true);
            //        ArchiveCardsListView.SelectedItem = null;
            //        await PopupNavigation.Instance.PushAsync(cardLimitsPopup);
            //    }
            //};
        }

        public async void LimitsPopupShow(ArchiveCardLimits card)
        {
                var cardLimitsPopup = new CardLimitsPopup(card.Limits, false, true);
                ArchiveCardsListView.SelectedItem = null;
                await PopupNavigation.Instance.PushAsync(cardLimitsPopup);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await viewModel.LoadArchiveCardLimits(null);
            UserDialogs.Instance.HideLoading();
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ArchiveCardsListView.ItemsSource = viewModel.ItemSource.FindAll(i => i.CardNumber.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.RequestLimits, _filterParams, ApplyFilters));
        }

        public async void ApplyFilters()
        {
            await viewModel.LoadArchiveCardLimits(_filterParams);
        }
    }
}