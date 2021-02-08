using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SI_Master.Helpers;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.REST;
using SI_Master.Services;
using SI_Master.Settings;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthService))]
namespace SI_Master.Services
{
    public class AuthService : IAuthService
    {

        public const string KEY_TOKEN = "auth_token";
        public const string KEY_CLIENT = "client";

        private readonly ISettings _settings = CrossSettings.Current;
        private readonly IAuthSettings _authsetting = DependencyService.Get<IAuthSettings>();
        //private readonly IApiProvider _apiprov = DependencyService.Get<IApiProvider>();
        private readonly IAuthApi _authApi = DependencyService.Get<IApiProvider>().GetAuthApi();
        private readonly IAuthManager _authDataProvider = DependencyService.Get<IAuthManager>();

        public string GetClient() => _settings.GetValueOrDefault(KEY_CLIENT, "");

        public async Task Login(string login, string phone)
        {
            var authData = _authDataProvider.GetAuthData();
            await _authApi.Register(new RegisterRequest
            {
                Login = login,
                PhoneNumber = phone,
                Shared = authData.Shared,
                SharedId = authData.SharedId,
                DeviceId = authData.DeviceId
            });
        }

        public async Task Verify(string login, string code, string phone)
        {
            var authData = _authDataProvider.GetAuthData();
            var response = await _authApi.Verify(new VerifyRequest
            {
                DeviceModel = DeviceInfo.Model,
                Login = login,
                Code = code,
                Phone = phone,
                Shared = authData.Shared,
                SharedId = authData.SharedId,
                DeviceId = authData.DeviceId
            });
            if (response.Result && response.Data != null)
            {
                UserAuthData user = new UserAuthData()
                {
                    Caption = "Новый пользователь", // ??? get from response.Data
                    Client = response.Data.Client,
                    Token = response.Data.Token
                };
                _authsetting.Active = _authsetting.AddOrUpdate(user);
                _settings.AddOrUpdateValue(KEY_CLIENT, response.Data.Client);
                _settings.AddOrUpdateValue(KEY_TOKEN, response.Data.Token);
            }
            else
            {
                throw new RequestUnsuccessfulException(response.Message);
            }
        }

        public void LogOut()
        {
            // TODO send request to the backend to reset registration
            _settings.AddOrUpdateValue(KEY_CLIENT, "");
            _settings.AddOrUpdateValue(KEY_TOKEN, "");
        }

        public bool IsAuthorized() => !string.IsNullOrEmpty(GetClient());
    }
}
