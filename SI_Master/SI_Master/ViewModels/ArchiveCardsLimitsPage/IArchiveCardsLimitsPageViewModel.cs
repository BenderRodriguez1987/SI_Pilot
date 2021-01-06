using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.ArchiveCardsLimitsPage
{
    public interface IArchiveCardsLimitsPageViewModel
    {
        string Title { get; set; }
        List<ArchiveCardLimits> ItemSource { get; set; }
        Task LoadArchiveCardLimits(Dictionary<string, object> _filterParams);
    }
}
