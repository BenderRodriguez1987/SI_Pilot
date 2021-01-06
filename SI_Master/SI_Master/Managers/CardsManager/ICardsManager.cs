using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Managers.CardsManager
{
    public interface ICardsManager
    {
        Task<List<Card>> GetCrds(Dictionary<string, object> _filterParams);
        Task<DictionaryLimits> GetDictionaryLimits();
        Task<DictionaryLimits> GetLimitsFromProvider(string provider);
    }
}
