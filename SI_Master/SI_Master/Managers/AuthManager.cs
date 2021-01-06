using SI_Master.Common;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.REST;
using SI_Master.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;

[assembly: Dependency(typeof(AuthManager))]
namespace SI_Master.Managers
{
    public class AuthManager : IAuthManager
    {
        INetworkService netwrokSevice = DependencyService.Get<INetworkService>();
        IDeviceInfo deviceInfo = DependencyService.Get<IDeviceInfo>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();

        private const string Shared = "MltOzaOAUlSpDwJ3IgYMVOukOD18vO9gefodGe5i57M=";
        private const string SharedID = "8";

        public async Task<Answer> Login(UserAuthData user)
        {
            object[] args = new object[1];
            args[1] = user;
            //Answer answer = await netwrokSevice.NetworkRequest(NetworkService.TaskType.Login, null , user, null);
            Answer answer = new Answer();
            return answer;
        }

        public async Task<Answer> RegisterDevice(string login, string phoneNumber)
        {
            DeviceRegistration registrationData = new DeviceRegistration(){
                //MOK
                Login = login,
                Shared = Shared,
                SharedId = SharedID,
                DeviceId = deviceInfo.GetIdentifier(),
                DeviceModel = DeviceInfo.Model,
                PhoneNumber = phoneNumber,
            };
            var answer = await netwrokSevice.Login(registrationData);
            return answer;
        }

        public async Task<Answer> CheckUserCredentials(string client, string token)
        {
            AuthData authData = new AuthData();
            authData = GetAuthData();
            if (!string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(token))
            {
                authData.Client = client;
                authData.Token = token;
            }
            var answer = await netwrokSevice.CheckUserCredentials(authData);
            return answer;
        }

        public AuthData GetAuthData()
        {
            UserAuthData currentUser = authSettings.ActiveUser();
            AuthData authData = new AuthData()
            {
                //Client = currentUser.Client,
                //Token = currentUser.Token,
                //Shared = Shared,
                //SharedId = SharedID,
                //DeviceId = deviceInfo.GetIdentifier(),
                Client = "329bd8",
                Token = "h4+N7hREXT39jNIdjr1L8j9GaNnAIFcxorlDZRR2Jc0=",
                Shared = Shared,
                SharedId = SharedID,
                DeviceId = "device_navigator",
            };

            //{
            //    Client = "329BD8",
            //    Token = "w+XZZ0xkFXGo3ojUJ2DciPxeWMuX45mPe9s7vV9tZHM=",
            //    Shared = "MltOzaOAUlSpDwJ3IgYMVOukOD18vO9gefodGe5i57M=",
            //    SharedId = "8",
            //    DeviceId = "35744307a4c44e6d",
            //    //DeviceId = deviceInfo.GetIdentifier()
            //};
            
            return authData;
        }

        public async Task MemorizeFCMTken(string FCMToken)
        {
            AuthData user = GetAuthData();
            FCMAuthData fcmAuthdata = new FCMAuthData()
            {
                Client = user.Client,
                Token = user.Token,
                Shared = user.Shared,
                SharedId = user.SharedId,
                DeviceId = "device_navigator",
                FCMToken = FCMToken
            };
                //{
                //    Shared = Shared,
                //    SharedId = SharedID,
                //    DeviceId = deviceInfo.GetIdentifier()
                //};
                //    foreach(var data in authdata) {
                //    List<string> clientsList = new List<string>();
                //    clientsList.Add(data.Client);
                //    fcmAuthdata.Client = data.Client;
                //    List<string> tokenList = new List<string>();
                //    tokenList.Add(data.Token);
                //    fcmAuthdata.Token = data.Token;
                //    fcmAuthdata.FCMToken = CrossFirebasePushNotification.Current.Token;
                //};
               await netwrokSevice.MemorizeFCMToken(fcmAuthdata);
        }

        public async Task ForgetFCMToken(List<UserAuthData> authdata)
        {
            FCMAuthData fcmAuthdata = new FCMAuthData()
            {
                Shared = Shared,
                SharedId = SharedID,
                DeviceId = deviceInfo.GetIdentifier()
            };
            foreach (var data in authdata)
            {
                List<string> clientsList = new List<string>();
                clientsList.Add(data.Client);
                fcmAuthdata.Client = data.Client;
                List<string> tokenList = new List<string>();
                tokenList.Add(data.Token);
                fcmAuthdata.Token = data.Token;
                //fcmAuthdata.FCMToken = CrossFirebasePushNotification.Current.Token;
            };
            await netwrokSevice.ForgetFCMToken(fcmAuthdata);
        }
    }
}

