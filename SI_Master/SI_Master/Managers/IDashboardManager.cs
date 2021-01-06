using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers
{
    public interface IDashboardManager
    {
        Task<AboutOrganozation> GetDashboard();
        Task<List<Contracts>> GetDesktop();
        Task<long> GetMobileCode();
        Task OrderKeys(int keyAmount);
    }
}
