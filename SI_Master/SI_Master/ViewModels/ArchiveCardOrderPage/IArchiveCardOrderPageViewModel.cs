using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.ArchiveCardOrderPage
{
    public interface IArchiveCardOrderPageViewModel
    {
        string Title { get; set; }
        List<CardOrder> ItemSource { get; set; }
        Task LoadArchiveCardOrder(Dictionary<string, object> _filterParams);
    }
}
