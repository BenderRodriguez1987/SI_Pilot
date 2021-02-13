using SI_Master.Models;
using SI_Master.REST;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;
using Xamarin.Forms;
using System.Linq;
using System.IO;
using SI_Master.Exceptions;

[assembly: Dependency(typeof(NetworkService))]
namespace SI_Master.REST
{
    public class NetworkService : INetworkService
    {

        public static readonly int OK = 0;
        private static readonly int NOT_AUTHORIZED = 401;
        private const string baseUrl = "https://mobilebakend.soft-enginiring.ru";

        public static int NO_CONNECTION = -1;
        public static int SERVER_ERROR = -2;
        public static int INTERNAL_ERROR = -3;
        public static int API_ERROR = -4;
        CookieContainer cookieContainer;
        private Api api;

        HttpClient client;
        private bool Initialized = false;

        public enum TaskType
        {
            Login,
            Getdashboard,
            OrderKeys,
            MobileAppCode,
            GetDashBoardMisc,
            Cards,
            CardLimits,
            CardOrder,
            LockAndUbnlockCards,
            SetCardHolder,
            Transactions,
            Settlements,
            Documents,
            DocumentsBill,
            DocumentsDownload,
            DocumentsSI,
            ARchiveCardBlock,
            ArchiveLimits,
            ArchiveCards,
            Contracts,
            PromisedPayment,
            PromesidPaymentAplly,
            DictionaryLimits,
            LockOrUnlockCard
            //CheckUserCredentials
        }

        private void Init(string Url)
        {

            cookieContainer = new CookieContainer();
            var handler = new LoggingHandler(new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            });
            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(Url),
                Timeout = new TimeSpan(0, 0, 120)
            };
            api = RestService.For<Api>(client);

            Initialized = true;
        }


        public async Task<Answer> NetworkRequest(TaskType type, AuthData authData, Dictionary<string, object> filterparams)
        {
            if(!Initialized)
            {
                Init(baseUrl);
            }
            Answer answer = null;
                switch (type)
            {
                //case TaskType.Login:
                //    //TODO Login
                //    break;
                case TaskType.Getdashboard:
                    answer = await api.Getdashboard(authData);
                    break;
                case TaskType.Cards:
                    answer = await api.Cards(filterparams, authData);
                    break;
                case TaskType.Transactions:
                    answer = await api.Transactions(filterparams, authData);
                    break;
                case TaskType.Settlements:
                    answer = await api.Settlements(filterparams, authData);
                    break;
                case TaskType.ArchiveLimits:
                    answer = await api.ArchiveLimits(filterparams, authData);
                    break;
                case TaskType.ARchiveCardBlock:
                    answer = await api.ARchiveCardBlock(filterparams, authData);
                    break;
                case TaskType.ArchiveCards:
                    answer = await api.ArchiveCards(filterparams, authData);
                    break;
                case TaskType.DictionaryLimits:
                    answer = await api.DictionaryLimits(authData);
                    break;
                //case TaskType.CheckUserCredentials:
                //    answer = await api.CheckUserCredentials(authData);
                //    break;
                case TaskType.OrderKeys:
                    answer = await api.OrderKeys(filterparams, authData);
                    break;
                case TaskType.MobileAppCode:
                    answer = await api.MobileAppCode(authData);
                    break;

            }
            return answer;
        }

        public async Task<Answer> CheckUserCredentials(AuthData authData)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            Answer answer = new Answer();
            try
            {
                var req = await api.CheckUserCredentials(authData);
            } catch (Exception e) {
                return ErrorHandler.HandleException(e);
            }
            return answer;
        }

        public async Task<Answer> GetQRCode(AuthData authData, Geoposition geopos)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            Answer answer = new Answer();
            try
            {
                answer = await api.GetQRCode(authData, geopos);

            }
            catch (Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
            return answer;
        }



        public async Task<Answer> GetWorksFromVisitId(AuthData authData, string visit_id)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            Answer answer = new Answer();
            try
            {
                answer = await api.GetNegotiateWorks(authData, visit_id);

            }
            catch (Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
            return answer;
        }

        public async Task<Answer> Login(DeviceRegistration registrationData)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            
            AuthData authData = new AuthData();
            Answer answer = new Answer();
            try
            {
                authData = await api.Login(registrationData);
                answer.ResType = OK;
                answer.ResData = authData;

            } catch (Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
            return answer;
        }

        public async Task<bool> SendCardLimits(Dictionary<string, string> sendingDic, AuthData authData)
        {
            if (!Initialized) {
                Init(baseUrl);
            }
            try {
                var answer = await api.SendCardLimits(sendingDic, authData);
                return true;
            } catch (Exception e) {
                ErrorHandler.HandleException(e);
                return false;
            }
        }

        public async Task<bool> LockOrUnlockCard(Dictionary<string, string> cardLock, AuthData authData)
        {
            if (!Initialized) {
                Init(baseUrl);
            }
            try {
                var answer = await api.LockOrUbnlockCards(cardLock, authData);
                return true;
            } catch (Exception e){
                ErrorHandler.HandleException(e);
                return false;
            }
        }

        public async Task<Answer> MemorizeFCMToken(FCMAuthData authData)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            try
            {
                var answer = await api.MemorizeFCMToken(authData);
                return answer;
            } catch ( Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
        }

        public async Task<Answer> ForgetFCMToken(FCMAuthData authData)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            try
            {
                var answer = await api.ForgetFCMToken(authData);
                return answer;
            }
            catch (Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
        }

        public async Task<Answer> GetOrderState(AuthData authData, string visit_id)
        {
            if (!Initialized)
            {
                Init(baseUrl);
            }
            Answer answer = new Answer();
            try
            {
                answer = await api.GetOrderState(authData, visit_id);
            }
            catch (Exception e)
            {
                return ErrorHandler.HandleException(e);
            }
            return answer;
        }
    }
}
