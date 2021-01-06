using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.ArchiveCardBlockPage
{
    public interface IArchiveCardBlockViewModel
    {
        string Title { get; set; }
        List<Block> ItemSource { get; set; }
        Task LoadArchiveCardBlock(Dictionary<string, object> _filterParams);
        
    }
}
