using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.DashboardPage
{
    public interface IDashboardPageViewModel
    {
        string Title { get; set; }
        AboutOrganozation Dashboard { get; set; }
        Task LoadDashboard();
    }
}
