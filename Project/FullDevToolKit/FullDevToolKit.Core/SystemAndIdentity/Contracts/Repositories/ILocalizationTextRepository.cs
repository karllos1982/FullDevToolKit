using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface ILocalizationTextRepository :
        IRepository<LocalizationTextParam, LocalizationTextEntry,
            LocalizationTextList, LocalizationTextResult >
    {

        Task<List<LocalizationTextList>> GetListOfLanguages();
    }
}
