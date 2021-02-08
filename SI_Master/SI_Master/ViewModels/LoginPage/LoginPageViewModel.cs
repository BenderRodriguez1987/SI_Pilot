using SI_Master.Helpers;
using SI_Master.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SI_Master.ViewModels.LoginPage
{
    public class LoginPageViewModel : BaseViewModel
    {

        private const string REGISTER_FAIL = "Не удалось выполнить запрос не регистрацию";

        private readonly IAuthService _authService = DependencyService.Get<IAuthService>();
        private readonly IToastService _toastService = DependencyService.Get<IToastService>();

        public LoginPageViewModel()
        {
            ShowLoginGroup();
        }

        private bool _loginGroupVisible = false;
        private bool _loginInvalidCaption = false;
        private bool _posInvalidCaption = false;
        private bool _phoneInvalidCaprion = false;

        public bool LoginGroupVisible
        {
            get { return _loginGroupVisible; }
            set { SetProperty(ref _loginGroupVisible, value); }
        }

        public bool LoginInvalidCaption
        {
            get { return _loginInvalidCaption; }
            set { SetProperty(ref _loginInvalidCaption, value); }
        }

        public bool PhoneInvalidCaprion
        {
            get { return _phoneInvalidCaprion; }
            set { SetProperty(ref _phoneInvalidCaprion, value); }
        }

        public bool PosInvalidCaption
        {
            get { return _posInvalidCaption; }
            set { SetProperty(ref _posInvalidCaption, value); }
        }

        public async Task<bool> Login(string login, string phone)
        {
            if (Validate(login, phone))
            {
                try
                {
                    ShowBusy();
                    await _authService.Login(login, phone);
                    ShowLoginGroup();
                    return true;
                }
                catch (Exception e)
                {
                    ShowLoginGroup();
                    if (e is RequestUnsuccessfulException)
                    {
                        _toastService.ShowToast(e.Message);
                    }
                    else
                    {
                        _toastService.ShowToast(REGISTER_FAIL);
                    }
                    return false;
                }
            }
            return false;
        }

        private bool Validate(string login, string phone)
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(login))
            {
                LoginInvalidCaption = true;
                isValid = false;
            }
            else
            {
                LoginInvalidCaption = false;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                PhoneInvalidCaprion = true;
                isValid = false;
            }
            else
            {
                PhoneInvalidCaprion = false;
            }
            return isValid;
        }

        private void ShowBusy()
        {
            IsBusy = true;
            LoginGroupVisible = false;
        }

        private void ShowLoginGroup()
        {
            IsBusy = false;
            LoginGroupVisible = true;
        }

    }
}
