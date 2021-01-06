using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers
{
    public interface IAuthManager
    {
        Task<Answer> Login(UserAuthData user);
        Task<Answer> RegisterDevice(string login, string phoneNumber);
        Task<Answer> CheckUserCredentials(string client, string token);
        AuthData GetAuthData();

        Task MemorizeFCMTken(string FCMToken);
        Task ForgetFCMToken(List<UserAuthData> authdata);
    }
}
