using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface ILocalizationTextDomain :
        IDomainSearchPaged<LocalizationTextParam, LocalizationTextEntry, LocalizationTextList, LocalizationTextResult>
    {

    }
}

