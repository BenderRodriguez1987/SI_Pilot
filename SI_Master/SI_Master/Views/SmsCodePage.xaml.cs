using Acr.UserDialogs;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SI_Master.Managers;
using SI_Master.ViewModels;
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
    public partial class SmsCodePage : ContentPage
    {

        private static readonly ISettings _settings = CrossSettings.Current;
        private const string KEY_TOKEN = "token";
        IAuthManager authmanager = DependencyService.Get<IAuthManager>();

        private const string ERROR = "Ошибка";

        private readonly SmsCodePageViewModel _viewModel;
        private string ph;
        public SmsCodePage(string login, string phone)
        {
            InitializeComponent();
            _viewModel = new SmsCodePageViewModel(login, phone);
            BindingContext = _viewModel;
            ph = phone;
        }

        private async void VerifyButton_Clicked(object sender, EventArgs e)
        {
            var code = codeEntry.Text;

            if (!_viewModel.Validate(code))
            {
                return;
            }

            var verifyResult = await _viewModel.Verify(code, ph);
            if (verifyResult.IsSuccess)
            {
                string fctkn =  _settings.GetValueOrDefault(KEY_TOKEN, "");
                authmanager.MemorizeFCMTken(fctkn);
                OpenMainPage();
            }
            else
            {
                codeEntry.Text = string.Empty;
                ShowError(verifyResult.Message);
            }
        }

        private async void LoginHelp_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://soft-enginiring.ru/forget-password/"));
        }

        private async void PhoneLabel_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:88003015715"));
        }

        private async void PrivacyPolicyLabel_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://soft-enginiring.ru/confidential.pdf"));
        }

        private void OpenMainPage()
        {
            Application.Current.MainPage = new MainPage(0);
        }

        private void ShowError(string message)
        {
            UserDialogs.Instance.Alert(message, ERROR);
        }
    }
}