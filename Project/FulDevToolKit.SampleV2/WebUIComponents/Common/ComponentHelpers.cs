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

namespace WebUIComponents.Common
{
   
    public class UserViewDetailsObject
    {
        public UserAuthenticated LoggedUser { get; set; }

        public DefaultLocalization Localization { get; set; }
                       
        public List<LocalizationTextList> LanguageList { get; set; }
                
        public AuthProxy Service { get; set; }

        public string Language { get; set; }
    }



}
