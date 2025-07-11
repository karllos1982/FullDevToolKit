using FullDevToolKit.Core;
using FullDevToolKit.Common;
using FullDevToolKit.ApplicationHelpers;
using FullDevToolKit.Core.Helpers;


namespace MyApp.API
{
    public class MyAppSettings : ISettings
    {

        private IConfiguration _env;

        public MyAppSettings()
        {
            
        }

        public MyAppSettings(IConfiguration webhost)
        {
            _env = webhost;
            LoadSettings();

        }

        public ConnectionStringManager Connections { get; set; }

        public string SiteURL { get; set; }

        public string ProfileImageDir { get; set; }

        public string FileStorageConnection { get; set; }

        public string ApplicationName { get; set; }

        public MailSettings MailSettings { get; set; }

        public string LocalizationLanguage { get; set; }

        public int ContextLength { get; set; }

        public void LoadSettings()
        {
            this.Connections = new ConnectionStringManager();
            this.Connections.AddConnection( ENUM_CONNECTIONSNAMES.MASTERDB, _env[ENUM_CONNECTIONSNAMES.MASTERDB]);
            this.Connections.AddConnection(ENUM_CONNECTIONSNAMES.SLAVE01DB, _env[ENUM_CONNECTIONSNAMES.SLAVE01DB]);

            this.SiteURL = _env["SiteURL"];
            this.ProfileImageDir = _env["ProfileImageDir"];
            this.FileStorageConnection = _env["FileStorageConnection"];
            this.ApplicationName = _env["ApplicationName"];            
            this.LocalizationLanguage = _env["LocalizationLanguage"];
                                   
            this.MailSettings = new MailSettings();
            this.MailSettings.SMTPServer = _env["SMTPServer"];
            this.MailSettings.SMTPUser = _env["SMTPUser"];
            this.MailSettings.SMTPPassword = _env["SMTPPassword"];
            this.MailSettings.Port = _env["Port"];
            this.MailSettings.EmailSender = _env["EmailSender"];
            this.MailSettings.NameSender = _env["NameSender"];

        }


    }
}
