using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{
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

    public class RoleEntry
    {
        public RoleEntry()
        {
            
        }

        public RoleEntry(RoleResult fromobj)
        {
            RoleID = fromobj.RoleID;
            RoleName = fromobj.RoleName;    
            CreateDate = fromobj.CreateDate;
            IsActive = fromobj.IsActive;
        }


        [PrimaryValidationConfig("RoleID", "Role ID", FieldType.NUMERIC, false, 0)]
        public long RoleID { get; set; }

        [PrimaryValidationConfig("RoleName", "Role Name", FieldType.TEXT, false, 50)]
        public string RoleName { get; set; } = string.Empty;    

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }
      
    }

    public class RoleList
    {
        
        public long RoleID { get; set; }
        
        public string RoleName { get; set; } = string.Empty;    

        public string sRoleID { get; set; } = string.Empty;
    }

    public class RoleResult
    {
        
        public long RoleID { get; set; }
        
        public string RoleName { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }
        

    }


}
