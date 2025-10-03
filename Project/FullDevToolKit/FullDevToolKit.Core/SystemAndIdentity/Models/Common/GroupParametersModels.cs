using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Common
{

    public class GroupParameterBaseModel: BaseModel
    {
        [PrimaryValidationConfig("GroupParameterID", "Group Parameter ID", FieldType.NUMERIC, false, 0)]
        public long GroupParameterID { get; set; }

        [PrimaryValidationConfig("GroupParameterName", "Group Parameter Name", FieldType.TEXT, false, 100)]
        public string GroupParameterName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

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

    public class GroupParameterEntry: GroupParameterBaseModel
    {
        public GroupParameterEntry()
        {
                
        }

        public GroupParameterEntry(GroupParameterResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }             

    }


    public class GroupParameterList: GroupParameterBaseModel
    {
        
    }

    public class GroupParameterResult: GroupParameterBaseModel
    {        

    }
}
