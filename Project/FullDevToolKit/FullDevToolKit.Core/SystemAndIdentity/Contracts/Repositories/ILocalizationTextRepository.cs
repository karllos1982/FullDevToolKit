using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Common;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface ILocalizationTextRepository :
        IRepository<LocalizationTextParam, LocalizationTextEntry, LocalizationTextResult, LocalizationTextList>
    {

        Task<List<LocalizationTextList>> GetListOfLanguages();
    }
}
