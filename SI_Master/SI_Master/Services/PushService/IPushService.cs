using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Services.PushService
{
    public interface IPushService
    {
        void OnDataPushReceived(IDictionary<string, string> data);
    }
}
