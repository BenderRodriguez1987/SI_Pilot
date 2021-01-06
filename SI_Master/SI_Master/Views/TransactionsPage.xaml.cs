using Acr.UserDialogs;
using SI_Master.Common;
using SI_Master.ViewModels.TransactionsPage;
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
    public partial class TransactionsPage : ContentPage
    {

        ITransactionsPageViewModel viewModel = DependencyService.Get<ITransactionsPageViewModel>();
        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();
        public TransactionsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(viewModel.ItemSource == null)
            {
                UserDialogs.Instance.ShowLoading();
                await viewModel.LoadTransactions(null);
                UserDialogs.Instance.HideLoading();
            }
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            TransactionsListView.ItemsSource = viewModel.ItemSource.FindAll(i => i.CardNumber.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.Transactions, _filterParams, ApplyFilters));
        }

        public async void ApplyFilters()
        {
            await viewModel.LoadTransactions(_filterParams);
        }
    }
}