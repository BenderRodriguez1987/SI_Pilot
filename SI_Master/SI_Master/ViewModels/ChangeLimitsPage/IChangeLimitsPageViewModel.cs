using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.ChangeLimitsPage
{
    public interface IChangeLimitsPageViewModel
    {
        void SetDictionary(DictionaryLimits dictionaryLimits, List<Limits> li);
        string Title { get; set; }

        List<LimitsItem> DataSource { get; set; }
        Task SendCardLimits(SendCardLimits sendCardLimits);

    }
}
