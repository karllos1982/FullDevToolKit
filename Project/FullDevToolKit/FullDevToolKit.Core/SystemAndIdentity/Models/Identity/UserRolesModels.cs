using FullDevToolKit.Common; 

namespace FullDevToolKit.System.Models.Identity
{
    public class UserRolesParam
    {

        public UserRolesParam()
        {
            pUserRoleID = 0;
            pUserID = 0;
            pRoleID = 0; 
        }

        public Int64 pUserRoleID { get; set; }

        public Int64 pUserID { get; set; }

        public Int64 pRoleID { get; set; }
    }

    public class UserRolesEntry
    {
        public Int64 UserRoleID { get; set; }

        public Int64 UserID { get; set; }

        public Int64 RoleID { get; set; }

        public RECORDSTATEENUM RecordState { get; set; }
    }

    public class UserRolesList
    {
        public Int64 UserRoleID { get; set; }

        public Int64 RoleID { get; set; }

        public string RoleName { get; set; }

    }

    public class UserRolesResult
    {
        public Int64 UserRoleID { get; set; }

        public Int64 UserID { get; set; }

        public string UserName { get; set; }

        public Int64 RoleID { get; set; }

        public string RoleName { get; set; }
        

    }

}
