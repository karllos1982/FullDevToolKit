using System;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Sys.Models.Common
{
    

    public class LanguageModels : BaseModel
    {
        [PrimaryValidationConfig("LanguageID", "Language ID", FieldType.NUMERIC, false, 0)]
        public long LanguageID { get; set; }

        [PrimaryValidationConfig("LanguageName", "Language Name", FieldType.TEXT, false, 5)]
        public string LanguageName { get; set; } = string.Empty;

        [PrimaryValidationConfig("Description", "Description", FieldType.TEXT, false, 255)]
        public string Description { get; set; } = string.Empty;
        
    }

}
