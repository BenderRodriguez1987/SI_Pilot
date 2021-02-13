using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.QRCodePage
{
    interface IQRCodePageViewModel
    {
        Task<string> LoadQRCode();
        Task<NegotiatedOrder> LoadWorks(string visit_id);

        Task<VisitStatus> LoadOrderState(string visit_id);
    }
}
