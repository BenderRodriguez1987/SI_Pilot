using Plugin.Settings.Abstractions;
using Refit;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.DeskTopPage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeskTopPageViewModel))]
namespace SI_Master.ViewModels.DeskTopPage
{
    public class DeskTopPageViewModel : BaseViewModel, IDeskTopPageViewModel
    {

        IDashboardManager dashboardManager = DependencyService.Get<IDashboardManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        private int UserIdforCurrentSource { get; set; } = 0;

        public string Title { get; set; } = "Рабочий стол";

        private List<Contracts> _ItemSource;
        public List<Contracts> ItemSource { get {
                return _ItemSource;
            } set {
                _ItemSource = value;
                OnPropertyChanged("ItemSource");
            } }

        public DeskTopPageViewModel()
        {
            //MOK
            //ItemSource = new ObservableCollection<Contract>();
            //ItemSource.Add(new Contract
            //{
            //    Block = false,
            //    Provider = "Белкамнефть",
            //    Number = "666",
            //    Date = DateTime.Now,
            //});
            //ItemSource.Add(new Contract
            //{
            //    Block = true,
            //    Provider = "Волкамводку",
            //    Number = "777",
            //    Date = DateTime.Now,
            //});
        }

        public async Task LoadDesktop()
        {
            if(ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await dashboardManager.GetDesktop();
            }
            UserIdforCurrentSource = authSettings.Active;
        }

        public async Task<long> GetMobileAppCode()
        {
            long code = 0;
            code = await dashboardManager.GetMobileCode();
            return code;
        }
    }
}
