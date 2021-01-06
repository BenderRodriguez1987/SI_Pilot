using Acr.UserDialogs;
using SI_Master.Common;
using SI_Master.ViewModels.ArchiveCardOrderPage;
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
    public partial class ArchiveCardOrderPage : ContentPage
    {
        IArchiveCardOrderPageViewModel viewModel = DependencyService.Get<IArchiveCardOrderPageViewModel>();
        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();
        public ArchiveCardOrderPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await viewModel.LoadArchiveCardOrder(null);
            UserDialogs.Instance.HideLoading();
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ArchiveCardsListView.ItemsSource = viewModel.ItemSource.FindAll(i => i.Supplier.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.RequestCards, _filterParams, ApplyFilters));
        }

        public async void ApplyFilters()
        {
            await viewModel.LoadArchiveCardOrder(_filterParams);
        }
    }
}