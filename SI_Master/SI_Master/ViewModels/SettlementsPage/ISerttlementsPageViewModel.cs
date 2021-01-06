using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.SettlementsPage
{
    public interface ISerttlementsPageViewModel
    {
        string Title { get; set; }
        List<Settlement> ItemSource { get; set; }
        Task LoadSettlements(Dictionary<string, object> _filterParams);
        Totals Totals { get; set; }
        string DebtLabel { get; set; }
        float Debt { get; set; }
    }
}
