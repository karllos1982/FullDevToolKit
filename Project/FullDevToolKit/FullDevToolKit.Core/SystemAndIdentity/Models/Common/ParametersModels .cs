using FullDevToolKit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.System.Models.Common
{
    public class ParameterParam
    {
        public ParameterParam()
        {
            pParameterID = 0;
            pGroupParameterID = 0;
            pParameterName = "";
            pIsActive = false;
        }


        public Int64 pParameterID { get; set; }
        public Int64 pGroupParameterID { get; set; }

        public string pParameterName { get; set; }

        public bool pIsActive { get; set; }

    }

    public class ParameterEntry
    {
        [PrimaryValidationConfig("ParameterID", "Parameter ID", FieldType.NUMERIC, false, 0)]
        public Int64 ParameterID { get; set; }

        [PrimaryValidationConfig("GroupParameterID", "Group Parameter ID", FieldType.NUMERIC, false, 0)]
        public Int64 GroupParameterID { get; set; }

        [PrimaryValidationConfig("ParameterName", "Parameter Name", FieldType.TEXT, false, 100)]
        public string ParameterName { get; set; }

        public bool IsActive { get; set; }

    }


    public class ParameterList
    {
        public Int64 ParameterID { get; set; }

        public Int64 GroupParameterID { get; set; }

        public string ParameterName { get; set; }

    }

    public class ParameterResult
    {
        
        public Int64 ParameterID { get; set; }
        
        public Int64 GroupParameterID { get; set; }

        public string ParameterName { get; set; }

        public bool IsActive { get; set; }

    }
}
