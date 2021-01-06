using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Threading.Tasks;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.DashboardPage;
using Xamarin.Forms;

[assembly: Dependency(typeof(DashboardPageViewModel))]
namespace SI_Master.ViewModels.DashboardPage
{
    public class DashboardPageViewModel : BaseViewModel, IDashboardPageViewModel
    {
        public string Title { get; set; }  = "Карточка предприятия";
        IDashboardManager dashboardManager = DependencyService.Get<IDashboardManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        private int UserIdforCurrentSource { get; set; } = 0;

        private AboutOrganozation _Dashboard { get; set; }
        public AboutOrganozation Dashboard { get {
                return _Dashboard;
            } set {
                _Dashboard = value;
                OnPropertyChanged("Dashboard");
            } }

        public async Task<AboutOrganozation> Dachboard()
        {
            if (Dashboard == null || UserIdforCurrentSource != authSettings.Active)
            {
                Dashboard = await dashboardManager.GetDashboard();
                UserIdforCurrentSource = authSettings.Active;
            }
            return Dashboard;
        }
        public async Task LoadDashboard()
        {
            if(Dashboard == null || UserIdforCurrentSource != authSettings.Active)
            {
                Dashboard = await dashboardManager.GetDashboard();
                UserIdforCurrentSource = authSettings.Active;
            }
        }
    }
}

