using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SI_Master.Managers.QRCodeManager;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                Geoposition geopos = new Geoposition
                {
                    Latitude = location.Latitude.ToString(),
                    Long = location.Longitude.ToString()
                };
                answer = await networkservice.GetQRCode(authmanager.GetAuthData(), geopos);
                if (answer != null && answer.ResData is JObject jData)
                {
                     qrobj = JsonConvert.DeserializeObject<QRObject>(jData.ToString());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                Geoposition geopos = new Geoposition
                {
                    Latitude = "0.0",
                    Long = "0.0"
                };
                try
                {
                    answer = await networkservice.GetQRCode(authmanager.GetAuthData(), geopos);
                    if (answer != null && answer.ResData is JObject jData)
                    {
                        qrobj = JsonConvert.DeserializeObject<QRObject>(jData.ToString());
                    }
                } catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
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
