using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.QRCodeManager
{
    interface IQRCodeManager
    {
        Task<QRObject> GetQRCode();
        Task<NegotiatedOrder> GetWorks(string visit_id);

        Task<VisitStatus> GetOrderStatus(string visit_id);
    }
}
