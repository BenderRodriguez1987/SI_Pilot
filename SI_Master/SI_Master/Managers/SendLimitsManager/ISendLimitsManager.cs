using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.SendLimitsManager
{
    public interface ISendLimitsManager
    {
        Task SendCardLimits(SendCardLimits CardLimits);
        Task LockOrUnlockCard(Dictionary<string, Dictionary<string, string>> cardBlock);
    }
}
