using SI_Master.Models;
using SI_Master.ViewModels.DeskTopPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SI_Master.PopUps;
using Rg.Plugins.Popup.Services;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeskTopPage : ContentPage
    {
        IDeskTopPageViewModel _viewModel = DependencyService.Get<IDeskTopPageViewModel>();
        public DeskTopPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading();
            await _viewModel.LoadDesktop();
            UserDialogs.Instance.HideLoading();
        }


        private async void MobileCodeButton_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            long code = await _viewModel.GetMobileAppCode();
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("", code.ToString(), "Ok");
        }

        private async void GetKeysButton_Clicked(object sender, EventArgs e)
        {
            OrderKeysPopup keysPopup = new OrderKeysPopup();
            await PopupNavigation.Instance.PushAsync(keysPopup);
        }
    }

}