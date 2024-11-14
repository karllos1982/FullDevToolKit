using FullDevToolKit.Helpers;

namespace FullDevToolKit.System.Models.Common
{
    public class GroupParameterParam
    {
        public GroupParameterParam()
        {
            pGroupParameterID = 0;
            pGroupParameterName = "";
            pIsActive = false;
        }


        public Int64 pGroupParameterID { get; set; }

        public string pGroupParameterName { get; set; }

        public bool pIsActive { get; set; }

    }

    public class GroupParameterEntry
    {
        [PrimaryValidationConfig("GroupParameterID", "Group Parameter ID", FieldType.NUMERIC, false, 0)]
        public Int64 GroupParameterID { get; set; }

        [PrimaryValidationConfig("GroupParameterName", "Group Parameter Name", FieldType.TEXT, false,100)]
        public string GroupParameterName { get; set; }
       
        public bool IsActive { get; set; }        

    }


    public class GroupParameterList
    {
        public Int64 GroupParameterID { get; set; }

        public string GroupParameterName { get; set; }

    }

    public class GroupParameterResult
    {
        
        public Int64 GroupParameterID { get; set; }
        
        public string GroupParameterName { get; set; }

        public bool IsActive { get; set; }

    }
}
