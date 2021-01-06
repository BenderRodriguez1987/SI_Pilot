using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.ArchiveCardBlockManager
{
    public interface IArchiveCardBlockManager
    {
        Task<List<Block>> GetArchiveCardLimits(Dictionary<string, object> _filterParams);
    }
}
