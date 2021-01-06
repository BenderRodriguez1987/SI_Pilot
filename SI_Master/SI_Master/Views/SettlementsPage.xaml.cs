using Acr.UserDialogs;
using SI_Master.Common;
using SI_Master.ViewModels.SettlementsPage;
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
    public partial class SettlementsPage : ContentPage
    {

        ISerttlementsPageViewModel viewmodel = DependencyService.Get<ISerttlementsPageViewModel>();

        private Dictionary<string, object> _filterParams = new Dictionary<string, object>();

        public SettlementsPage()
        {
            InitializeComponent();
            BindingContext = viewmodel;
            _filterParams.Add("filters[date_to]", DateTime.Today);
            _filterParams.Add("filters[date_from]", DateTime.Today.AddMonths(-1));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await viewmodel.LoadSettlements(_filterParams);
            if (_filterParams.TryGetValue("filters[date_from]", out object dt) && dt is DateTime)
            {
                CurrentPeriodLabel.Text = string.Format("{0:yyyy-MM-dd}", dt);
            }
            if (_filterParams.TryGetValue("filters[date_to]", out object dt_to) && dt_to is DateTime)
            {
                CurrentPeriodLabel.Text = CurrentPeriodLabel.Text + " - " +string.Format("{0:yyyy-MM-dd}", dt_to);
            }
            if(viewmodel.Debt > 0) {
                DebtValueLabel.TextColor = Color.Green;
            } else  {
                DebtValueLabel.TextColor = Color.Red;
            }
            UserDialogs.Instance.HideLoading();
        }
        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            SettlementsList.ItemsSource = viewmodel.ItemSource.FindAll(i => i.Contract.Contains(e.NewTextValue));
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FiltersPage(QueryMode.Settlements, _filterParams, ApplyFilters));
        }

        public async void ApplyFilters()
        {
            await viewmodel.LoadSettlements(_filterParams);
        }
    }
}