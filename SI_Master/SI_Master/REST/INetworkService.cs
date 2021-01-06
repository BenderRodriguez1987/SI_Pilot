using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.REST
{
    public interface INetworkService
    {
        Task<Answer> NetworkRequest(NetworkService.TaskType type, AuthData authData, Dictionary<string, object> filterparams);
        Task<Answer> Login(DeviceRegistration registrationData);
        Task<Answer> GetQRCode(AuthData authData, Geoposition geopos);
        Task<Answer> GetWorksFromVisitId(AuthData authData, string visit_id);
        Task<Answer> CheckUserCredentials(AuthData authData);

        Task<bool> SendCardLimits(Dictionary<string, string> sendingDic, AuthData authData);
        Task<bool> LockOrUnlockCard(Dictionary<string, string> cardLock, AuthData authData);
        Task<Answer> MemorizeFCMToken(FCMAuthData authData);
        Task<Answer> ForgetFCMToken(FCMAuthData authData);
    }
}
