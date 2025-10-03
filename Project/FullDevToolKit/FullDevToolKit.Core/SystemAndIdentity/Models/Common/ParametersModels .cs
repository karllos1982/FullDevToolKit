using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Sys.Models.Common
{
    public class ParameterBaseModel: BaseModel
    {
        [PrimaryValidationConfig("ParameterID", "Parameter ID", FieldType.NUMERIC, false, 0)]
        public long ParameterID { get; set; }

        [PrimaryValidationConfig("GroupParameterID", "Group Parameter ID", FieldType.NUMERIC, false, 0)]
        public long GroupParameterID { get; set; }

        [PrimaryValidationConfig("ParameterName", "Parameter Name", FieldType.TEXT, false, 100)]
        public string ParameterName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class ParameterParam
    {
        public ParameterParam()
        {
            pParameterID = 0;
            pGroupParameterID = 0;
            pParameterName = "";
            pIsActive = false;
        }


        public long pParameterID { get; set; }
        public long pGroupParameterID { get; set; }

        public string pParameterName { get; set; }

        public bool pIsActive { get; set; }

    }

    public class ParameterEntry: ParameterBaseModel
    {
        public ParameterEntry()
        {
            
        }

        public ParameterEntry(ParameterResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }

     
    }


    public class ParameterList: ParameterBaseModel
    {  

    }

    public class ParameterResult: ParameterBaseModel
    {
        public string GroupParameterName { get; set; } = string.Empty;

    }
}
