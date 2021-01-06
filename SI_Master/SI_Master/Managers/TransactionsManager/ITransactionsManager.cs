using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.TransactionsManager
{
    interface ITransactionsManager
    {
        Task<List<Transaction>> Gettransactions(Dictionary<string, object> _filterParams);
    }
}
