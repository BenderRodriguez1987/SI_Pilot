using SI_Master.Managers.ArchiveCardLimitsManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.ArchiveCardsLimitsPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ArchiveCardsLimitsPageViewModel))]
namespace SI_Master.ViewModels.ArchiveCardsLimitsPage
{
    public class ArchiveCardsLimitsPageViewModel : BaseViewModel, IArchiveCardsLimitsPageViewModel
    {
        IArchiveCardLimitsManager archiveCardlimitsManager = DependencyService.Get<IArchiveCardLimitsManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        public string Title { get; set; } = "Заявки на лимиты";
        private int UserIdforCurrentSource { get; set; } = 0;
        private List<ArchiveCardLimits> _itemsource { get; set; }
        public List<ArchiveCardLimits> ItemSource { get {
                return _itemsource;
            }  set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
            } }

        public async Task LoadArchiveCardLimits(Dictionary<string, object> _filterParams)
        {
            if (ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await archiveCardlimitsManager.GetArchiveCardLimits(null);
                UserIdforCurrentSource = authSettings.Active;
            }
            if (_filterParams != null)
            {
                ItemSource = await archiveCardlimitsManager.GetArchiveCardLimits(_filterParams);
            }
        }
    }
}
