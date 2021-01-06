using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.ArchiveCardLimitsManager
{
    public interface IArchiveCardLimitsManager
    {
        Task<List<ArchiveCardLimits>> GetArchiveCardLimits(Dictionary<string, object> _filterParams);
    }
}
