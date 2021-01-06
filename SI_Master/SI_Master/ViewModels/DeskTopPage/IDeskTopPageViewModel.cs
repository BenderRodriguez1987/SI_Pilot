using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.DeskTopPage
{
    public interface IDeskTopPageViewModel
    {
        string Title { get; set; }
        List<Contracts> ItemSource { get; set; }
        Task LoadDesktop();
        Task<long> GetMobileAppCode();
    }
}
