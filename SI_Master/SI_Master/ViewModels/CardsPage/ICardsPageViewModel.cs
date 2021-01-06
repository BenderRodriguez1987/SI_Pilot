using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.CardsPage
{
    public interface ICardsPageViewModel
    {
        string Title { get; set; }

        List<Card> ItemSource { get; set; }

        Task LoadCards(Dictionary<string, object> _filterParams);
        Task<DictionaryLimits> GetLimitsFromProvider(string provider);
    }
}
