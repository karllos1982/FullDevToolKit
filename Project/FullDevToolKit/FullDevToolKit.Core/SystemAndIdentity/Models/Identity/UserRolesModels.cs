using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class UserRolesBaseModel: BaseModel
    {
        public long UserRoleID { get; set; }

        public long UserID { get; set; }

        public long RoleID { get; set; }

        public RECORDSTATEENUM RecordState { get; set; }
    }

    public class UserRolesParam
    {

        public UserRolesParam()
        {
            pUserRoleID = 0;
            pUserID = 0;
            pRoleID = 0; 
        }

        public long pUserRoleID { get; set; }

        public long pUserID { get; set; }

        public long pRoleID { get; set; }
    }

    public class UserRolesEntry : UserRolesBaseModel
    {
        public UserRolesEntry()
        {

        }

        public UserRolesEntry(UserRolesResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }

   
    }

    public class UserRolesList : UserRolesBaseModel
    {         

    }

    public class UserRolesResult : UserRolesBaseModel
    {
        public string UserName { get; set; } = string.Empty;
        
        public string RoleName { get; set; } = string.Empty;

    }

}
