using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface ILocalizationTextRepository :
        IRepositorySearchPaged<LocalizationTextParam, LocalizationTextEntry,
            LocalizationTextList, LocalizationTextResult >
    {

        Task<List<LocalizationTextList>> GetListOfLanguages();
    }
}
