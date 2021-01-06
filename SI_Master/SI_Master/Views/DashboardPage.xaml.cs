using Acr.UserDialogs;
using SI_Master.ViewModels.DashboardPage;
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
    public partial class DashboardPage : ContentPage
    {
        IDashboardPageViewModel viewmodel = DependencyService.Get<IDashboardPageViewModel>();
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = viewmodel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await viewmodel.LoadDashboard();
            UserDialogs.Instance.HideLoading();
        }
    }
}