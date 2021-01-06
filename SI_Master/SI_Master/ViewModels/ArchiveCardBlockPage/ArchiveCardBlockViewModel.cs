using SI_Master.Managers.ArchiveCardBlockManager;
using SI_Master.Models;
using SI_Master.Settings;
using SI_Master.ViewModels.ArchiveCardBlockPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ArchiveCardBlockViewModel))]
namespace SI_Master.ViewModels.ArchiveCardBlockPage
{
    public class ArchiveCardBlockViewModel : BaseViewModel, IArchiveCardBlockViewModel
    {

        IArchiveCardBlockManager archivCardBlockManager = DependencyService.Get<IArchiveCardBlockManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        public string Title { get; set; } = "Заявки на блокировку";
        private int UserIdforCurrentSource { get; set; } = 0;

        private List<Block> _itemsource { get; set; }
        public List<Block> ItemSource { get {
                return _itemsource;
            } set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
            } }

        public async Task LoadArchiveCardBlock(Dictionary<string, object> _filterParams)
        {
            if (ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await archivCardBlockManager.GetArchiveCardLimits(null);
                UserIdforCurrentSource = authSettings.Active;
            }
            if(_filterParams != null)
            {
                ItemSource = await archivCardBlockManager.GetArchiveCardLimits(_filterParams);
            }
        }
    }
}
