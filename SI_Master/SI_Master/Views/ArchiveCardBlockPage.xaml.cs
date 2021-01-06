using Acr.UserDialogs;
using SI_Master.Common;
using SI_Master.ViewModels.ArchiveCardBlockPage;
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
    public partial class ArchiveCardBlockPage : ContentPage
    {
        IArchiveCardBlockViewModel viewModel = DependencyService.Get<IArchiveCardBlockViewModel>();
        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();
        public ArchiveCardBlockPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await viewModel.LoadArchiveCardBlock(null);
            UserDialogs.Instance.HideLoading();
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ArchiveCardsListView.ItemsSource = viewModel.ItemSource.FindAll(i => i.CardNumber.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.RequestBlocking, _filterParams, ApplyFilters));
        }

        public async void ApplyFilters()
        {
            await viewModel.LoadArchiveCardBlock(_filterParams);
        }
    }
}