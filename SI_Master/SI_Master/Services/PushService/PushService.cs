using SI_Master.Services.PushService;
using SI_Master.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushService))]
namespace SI_Master.Services.PushService
{
    public class PushService : IPushService
    {

        private IAuthSettings authsetting = DependencyService.Get<IAuthSettings>();


        public const string VISIT_ID_KEY = "visit_id";
        public const string STATUS_KEY = "status";
        public const string QR_KEY = "qr";
        public const string STATUS_6 = "6";
        public const string STATUS_10 = "10";
        public const string STATUS_12 = "12";
        public const string STATUS_13 = "13";
        public void OnDataPushReceived(IDictionary<string, string> data)
        {

            if (data.ContainsKey(VISIT_ID_KEY) && data.ContainsKey(STATUS_KEY))
            {
                Dictionary<string, string> _data = new Dictionary<string, string>(data);
                authsetting.AddOrUpdateVisitId(_data);

                int.TryParse(data[STATUS_KEY], out int status);
                if (status == 6)
                {
                    MessagingCenter.Send(this, data[STATUS_KEY], data[VISIT_ID_KEY]);
                }
                if(status == 10)
                {
                    MessagingCenter.Send(this, data[STATUS_KEY], data[QR_KEY]);
                    System.Diagnostics.Debug.WriteLine(data[QR_KEY]);
                }
                if(status == 12)
                {
                    MessagingCenter.Send(this, data[STATUS_KEY], data[VISIT_ID_KEY]);
                }
                if (status == 13)
                {
                    authsetting.RemoveVisitId(data[VISIT_ID_KEY]);
                    MessagingCenter.Send(this, data[STATUS_KEY], data[VISIT_ID_KEY]);
                }
            }
        }
    }
}
