using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Managers.QRCodeManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(QRCodeManager))]
namespace SI_Master.Managers.QRCodeManager
{
    class QRCodeManager: IQRCodeManager
    {
        IAuthManager authmanager = DependencyService.Get<IAuthManager>();
        INetworkService networkservice = DependencyService.Get<INetworkService>();

        public async Task<VisitStatus> GetOrderStatus(string visit_id)
        {
            Answer answer = new Answer();
            VisitStatus qrobj = new VisitStatus();
            try
            {
                answer = await networkservice.GetOrderState(authmanager.GetAuthData(), visit_id);
                if (answer != null && answer.ResData is JObject jData)
                {
                    qrobj = JsonConvert.DeserializeObject<VisitStatus>(jData.ToString());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return qrobj;
        }

        async public Task<QRObject> GetQRCode()
        {
            Answer answer = new Answer();
            QRObject qrobj = new QRObject();
            try
            {
                Geoposition geopos = new Geoposition();
                geopos.Latitude = "56.85306";
                geopos.Long = "53.21222";

                answer = await networkservice.GetQRCode(authmanager.GetAuthData(), geopos);
                if (answer != null && answer.ResData is JObject jData)
                {
                     qrobj = JsonConvert.DeserializeObject<QRObject>(jData.ToString());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return qrobj;
        }

        public async Task<NegotiatedOrder> GetWorks(string visit_id)
        {
            Answer answer = new Answer();
            NegotiatedOrder order = new NegotiatedOrder();
            try {
                answer = await networkservice.GetWorksFromVisitId(authmanager.GetAuthData(), visit_id);
                if(answer != null && answer.ResData is JObject jData) 
                {
                    if (jData.TryGetValue("dataJson", out JToken jOrder))
                    {
                        order = JsonConvert.DeserializeObject<NegotiatedOrder>(jOrder.ToString());
                    }
                }
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return order;
        }
    }
}
