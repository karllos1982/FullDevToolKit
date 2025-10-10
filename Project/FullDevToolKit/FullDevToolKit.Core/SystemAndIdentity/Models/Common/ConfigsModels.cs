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
    public class ConfigsBaseModel : BaseModel
    {
        [PrimaryValidationConfig("ConfigID", "Config ID", FieldType.NUMERIC, false, 0)]
        public long ConfigID { get; set; }     

        [PrimaryValidationConfig("ConfigName", "Config Name", FieldType.TEXT, false, 50)]
        public string ConfigName { get; set; } = string.Empty;

        [PrimaryValidationConfig("ConfigValue", "Config Value", FieldType.TEXT, false, 255)]
        public string ConfigValue { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class ConfigsParam
    {
        public ConfigsParam()
        {
            pConfigID = 0;            
            pConfigName = "";
            pIsActive = false;
        }


        public long pConfigID { get; set; }
        
        public string pConfigName { get; set; }

        public bool pIsActive { get; set; }

    }

    public class ConfigsEntry : ConfigsBaseModel
    {
        public ConfigsEntry()
        {

        }

        public ConfigsEntry(ConfigsResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }


    }


    public class ConfigsList : ConfigsBaseModel
    {

    }

    public class ConfigsResult : ConfigsBaseModel
    {        

    }
}
