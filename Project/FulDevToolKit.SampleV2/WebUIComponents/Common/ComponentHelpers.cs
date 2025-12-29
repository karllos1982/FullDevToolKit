using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using MyApp.ViewModel;
using MyApp.Proxys;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullDevToolKit.Common;

namespace WebUIComponents.Common
{
   
    public class UserViewDetailsObject
    {
        public UserAuthenticated LoggedUser { get; set; }

        public LocalizationResource Localization { get; set; }
                       
        public List<LanguageList> LanguageList { get; set; }
                
        public AuthProxy Service { get; set; }

        public string Language { get; set; }
    }

    public enum ButtonTypeEnum
    {
        NONE = 0,
        SEARCH= 1,
        ADD= 2,
        SAVE= 3,
        DELETE=4
    }

    public class TaskLoadingConfigs
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class TimelineConfigs
    {
        public string Title { get; set; }

        public List<DataLogTimelineModel> TimeLineRecords { get; set; }

    }

    public class PersonContactDetailsConfigs
    {
        public string Title { get; set; }

        public PersonViewModel View { get; set; }

        public IDialogService msgbox { get; set; }
    }

    public class PaginatorPageSelector
    {
        public string Title { get; set; } = string.Empty;        

    }

    public class BaseEntryFieldMask
    {
        public string Title { get; set; } = string.Empty;
    }
}
