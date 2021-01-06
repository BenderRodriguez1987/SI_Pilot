using SI_Master.Managers.ArchiveCardOrderManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.ArchiveCardOrderPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ArchiveCardOrderPageViewModel))]
namespace SI_Master.ViewModels.ArchiveCardOrderPage
{
    public class ArchiveCardOrderPageViewModel : BaseViewModel, IArchiveCardOrderPageViewModel
    {
        IArchiveCradOrderManager cardOrderManager = DependencyService.Get<IArchiveCradOrderManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        public string Title { get; set; } = "Заявки на карты";
        private int UserIdforCurrentSource { get; set; } = 0;
        private List<CardOrder> _itemsource { get; set; }
        public List<CardOrder> ItemSource { get {
                return _itemsource;
            } set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
            } }

        public async Task LoadArchiveCardOrder(Dictionary<string, object> _filterParams)
        {
            if (ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await cardOrderManager.GetArchiveCardOrders(null);
                UserIdforCurrentSource = authSettings.Active;
            }
            if (_filterParams != null)
            {
                ItemSource = await cardOrderManager.GetArchiveCardOrders(_filterParams);
            }
        }
    }
}
