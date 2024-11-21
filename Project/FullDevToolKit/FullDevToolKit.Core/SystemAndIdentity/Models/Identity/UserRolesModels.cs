using FullDevToolKit.Common; 

namespace FullDevToolKit.Sys.Models.Identity
{
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

    public class UserRolesEntry
    {
        public UserRolesEntry()
        {

        }

        public UserRolesEntry(UserRolesResult fromobj)
        {
            UserRoleID = fromobj.UserRoleID;  
            UserID = fromobj.UserID;    
            RoleID = fromobj.RoleID;    
        }

        public long UserRoleID { get; set; }

        public long UserID { get; set; }

        public long RoleID { get; set; }

        public RECORDSTATEENUM RecordState { get; set; }
    }

    public class UserRolesList
    {
        public long UserRoleID { get; set; }

        public long RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;    

    }

    public class UserRolesResult
    {
        public long UserRoleID { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; } = string.Empty;    

        public long RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public RECORDSTATEENUM RecordState { get; set; }

    }

}
