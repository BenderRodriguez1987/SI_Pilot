using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.SettlementsManager
{
    public interface ISettlementsManager
    {
        Task<List<Settlement>> GetSettlements();
        Task<Settlements> GetSettlementsWithTotals(Dictionary<string, object> _filterParams);
    }
}
