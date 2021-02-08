using Refit;
using SI_Master.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SI_Master.ViewModels
{
    public class SmsCodePageViewModel: BaseViewModel
    {
        public class VerifyResult
        {
            public bool IsSuccess { get; set; }

            public string Message { get; set; }
        }

        private const string WRONG_CODE = "Введен неверный код";
        private const string REQUEST_ERROR = "Во время выполнения запроса произошла ошибка";

        private readonly string _login = string.Empty;
        private readonly string _pos = string.Empty;

        private readonly IAuthService _authService = DependencyService.Get<IAuthService>();

        private bool _smsCodeGroupVisible = false;
        private bool _codeInvalidCaption = false;

        public bool SmsCodeGroupVisible
        {
            get { return _smsCodeGroupVisible; }
            set { SetProperty(ref _smsCodeGroupVisible, value); }
        }

        public bool CodeInvalidCaption
        {
            get { return _codeInvalidCaption; }
            set { SetProperty(ref _codeInvalidCaption, value); }
        }

        public SmsCodePageViewModel(string login, string pos)
        {
            _login = login;
            _pos = pos;
            ShowSmsCodeGroup();
        }

        public async Task<VerifyResult> Verify(string code, string phone)
        {
            try
            {
                ShowBusy();
                await _authService.Verify(_login, code, phone);
                ShowSmsCodeGroup();
                return new VerifyResult
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                ShowSmsCodeGroup();
                if (e is ApiException apiException
                    && apiException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new VerifyResult
                    {
                        IsSuccess = false,
                        Message = WRONG_CODE
                    };
                }
                return new VerifyResult
                {
                    IsSuccess = false,
                    Message = REQUEST_ERROR
                };
            }
        }

        public bool Validate(string code)
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(code))
            {
                CodeInvalidCaption = true;
                isValid = false;
            }
            return isValid;
        }

        private void ShowBusy()
        {
            IsBusy = true;
            SmsCodeGroupVisible = false;
        }

        private void ShowSmsCodeGroup()
        {
            IsBusy = false;
            SmsCodeGroupVisible = true;
        }
    }
}
