using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class RoleBaseModel: BaseModel
    {
        [PrimaryValidationConfig("RoleID", "Role ID", FieldType.NUMERIC, false, 0)]
        public long RoleID { get; set; }

        [PrimaryValidationConfig("RoleName", "Role Name", FieldType.TEXT, false, 50)]
        public string RoleName { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }
    }

    public class RoleParam
    {

        public RoleParam()
        {
            pRoleID = 0;
            pRoleName = ""; 
        }

        public long pRoleID { get; set; }

        public string pRoleName { get; set; }

    }

    public class RoleEntry: RoleBaseModel
    {
        public RoleEntry()
        {
            
        }

        public RoleEntry(RoleResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }   
      
    }

    public class RoleList: RoleBaseModel
    {
             
    }

    public class RoleResult: RoleBaseModel
    {
                 

    }


}
