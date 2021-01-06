using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.TransactionsPage
{
    public interface ITransactionsPageViewModel
    {
        string Title { get; set; }
        List<Transaction> ItemSource { get; set; }
        Task LoadTransactions(Dictionary<string, object> _filterParams);
    }
}
