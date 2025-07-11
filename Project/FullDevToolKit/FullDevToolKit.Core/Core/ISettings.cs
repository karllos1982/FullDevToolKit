using FullDevToolKit.Common;
using FullDevToolKit.ApplicationHelpers;
using FullDevToolKit.Core.Helpers;

namespace FullDevToolKit.Core
{
    public interface ISettings
    {

         ConnectionStringManager Connections { get; set; }

         string SiteURL { get; set; }

         string ProfileImageDir { get; set; }

         string ApplicationName { get; set; }

         MailSettings MailSettings { get; set; }

         string LocalizationLanguage { get; set; }

         int ContextLength { get; set; }

    }
}
