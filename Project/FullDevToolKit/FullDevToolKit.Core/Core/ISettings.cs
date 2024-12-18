﻿using FullDevToolKit.Common;
using FullDevToolKit.ApplicationHelpers;

namespace FullDevToolKit.Core
{
    public interface ISettings
    {

         SourceConfig[] Sources { get; set; }

         string SiteURL { get; set; }

         string ProfileImageDir { get; set; }

         string ApplicationName { get; set; }

         MailSettings MailSettings { get; set; }

         string LocalizationLanguage { get; set; }

         int ContextLength { get; set; }

    }
}
