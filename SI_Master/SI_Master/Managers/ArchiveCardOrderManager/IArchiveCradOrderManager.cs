using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.ArchiveCardOrderManager
{
    public interface IArchiveCradOrderManager
    {
        Task<List<CardOrder>> GetArchiveCardOrders(Dictionary<string, object> _filterParams);
    }
}
