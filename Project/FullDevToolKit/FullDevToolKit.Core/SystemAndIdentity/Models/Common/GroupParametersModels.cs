using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Common
{
    public class GroupParameterParam
    {
        public GroupParameterParam()
        {
            pGroupParameterID = 0;
            pGroupParameterName = "";
            pIsActive = -1;
        }


        public long pGroupParameterID { get; set; }

        public string pGroupParameterName { get; set; }

        public int pIsActive { get; set; }

    }

    public class GroupParameterEntry
    {
        public GroupParameterEntry()
        {
                
        }

        public GroupParameterEntry(GroupParameterResult fromobj)
        {
            GroupParameterID = fromobj.GroupParameterID;    
            GroupParameterName = fromobj.GroupParameterName;
            IsActive = fromobj.IsActive;
        }

        [PrimaryValidationConfig("GroupParameterID", "Group Parameter ID", FieldType.NUMERIC, false, 0)]
        public long GroupParameterID { get; set; }

        [PrimaryValidationConfig("GroupParameterName", "Group Parameter Name", FieldType.TEXT, false,100)]
        public string GroupParameterName { get; set; } = string.Empty;

        public bool IsActive { get; set; }        

    }


    public class GroupParameterList
    {
        public long GroupParameterID { get; set; }

        public string GroupParameterName { get; set; } = string.Empty;

    }

    public class GroupParameterResult
    {
        
        public long GroupParameterID { get; set; }
        
        public string GroupParameterName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

    }
}
