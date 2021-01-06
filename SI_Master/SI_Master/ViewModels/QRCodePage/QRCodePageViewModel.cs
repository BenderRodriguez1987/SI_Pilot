using SI_Master.Managers.QRCodeManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.QRCodePage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(QRCodePageViewModel))]
namespace SI_Master.ViewModels.QRCodePage
{
    class QRCodePageViewModel: BaseViewModel, IQRCodePageViewModel
    {
        IQRCodeManager qrmanager = DependencyService.Get<IQRCodeManager>();

        public async Task<string> LoadQRCode()
        {
            QRObject qrobj = await qrmanager.GetQRCode();
            return qrobj.Qr;
        }

        public async Task<NegotiatedOrder> LoadWorks(string visit_id)
        {
            NegotiatedOrder order = await qrmanager.GetWorks(visit_id);
            return order;
        }
    }
}
