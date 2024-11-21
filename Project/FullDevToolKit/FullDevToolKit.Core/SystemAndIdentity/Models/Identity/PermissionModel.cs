using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{
    public class PermissionParam
    {
        public long pPermissionID { get; set; }

        public long pUserID { get; set; }

        public long pRoleID { get; set; }

        public long pObjectPermissionID { get; set; }

        public PermissionParam()
        {
            pPermissionID = 0;
            pUserID = 0;
            pRoleID = 0;
            pObjectPermissionID = 0;
        }

    }

    public class PermissionEntry
    {
        public PermissionEntry()
        {
            
        }

        public PermissionEntry(PermissionResult fromobj)
        {
            PermissionID = fromobj.PermissionID;
            ObjectPermissionID = fromobj.ObjectPermissionID;
            UserID = fromobj.UserID;
            RoleID = fromobj.RoleID;
            ReadStatus = fromobj.ReadStatus;    
            SaveStatus = fromobj.SaveStatus;
            DeleteStatus = fromobj.DeleteStatus;    
            TypeGrant = fromobj.TypeGrant;  
        }

        public long PermissionID { get; set; }

        [PrimaryValidationConfig("ObjectPermissionID", "Object Permission ID", FieldType.NUMERIC, false, 0)]
        public long ObjectPermissionID { get; set; }

        [PrimaryValidationConfig("RoleID", "Role ID", FieldType.NUMERIC, true, 0)]
        public long? RoleID { get; set; }

        [PrimaryValidationConfig("UserID", "User ID", FieldType.NUMERIC, true, 0)]
        public long? UserID { get; set; }

        [PrimaryValidationConfig("ReadStatus", "Read Status", FieldType.NUMERIC, false, 0)]
        public int ReadStatus { get; set; }       

        [PrimaryValidationConfig("SaveStatus", "Save Status", FieldType.NUMERIC, false, 0)]
        public int SaveStatus { get; set; }

        [PrimaryValidationConfig("DeleteStatus", "Delete Status", FieldType.NUMERIC, false, 0)]
        public int DeleteStatus { get; set; }

        [PrimaryValidationConfig("TypeGrant", "Type Grant", FieldType.TEXT, false,1 )]
        public string TypeGrant { get; set; } = string.Empty;
    }

    public class PermissionList
    {
        public long PermissionID { get; set; }

        public long ObjectPermissionID { get; set; }

        public string ObjectName { get; set; } = string.Empty;              


    }

    public class PermissionResult
    {
        public long PermissionID { get; set; }

        public long ObjectPermissionID { get; set; }

        public string ObjectName { get; set; } = string.Empty;  

        public string ObjectCode { get; set; } = string.Empty;  

        public long RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;    

        public long UserID { get; set; }

        public string UserName { get; set; } = string.Empty;    

        public int ReadStatus { get; set; }

        public int SaveStatus { get; set; }        

        public int DeleteStatus { get; set; }

        public string TypeGrant { get; set; } = string.Empty;   

    }
  

}
