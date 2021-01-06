using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using SI_Master.Managers.TransactionsManager;
using SI_Master.Models;
using SI_Master.ViewModels.TransactionsPage;
using SI_Master.Settings;

[assembly: Dependency(typeof(TransactionsPageViewModel))]
namespace SI_Master.ViewModels.TransactionsPage
{
    public class TransactionsPageViewModel : BaseViewModel, ITransactionsPageViewModel
    {

        ITransactionsManager transactionsManager = DependencyService.Get<ITransactionsManager>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        private int UserIdforCurrentSource { get; set; } = 0;

        public string Title { get; set; } = "Транзакции";
        private List<Transaction> _itemsource;
        public List<Transaction> ItemSource
        { get {
                return _itemsource;
            } set {
                _itemsource = value;
                OnPropertyChanged("ItemSource");
                  }
             }

        public async Task LoadTransactions(Dictionary<string, object> _filterParams)
        {
            if (ItemSource == null || UserIdforCurrentSource != authSettings.Active)
            {
                ItemSource = await transactionsManager.Gettransactions(null);
                UserIdforCurrentSource = authSettings.Active;
            }
            if (_filterParams != null)
            {
                ItemSource = await transactionsManager.Gettransactions(_filterParams);
            }
        }
    }
}
