using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class PermissionBaseModel: BaseModel
    {
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

        [PrimaryValidationConfig("TypeGrant", "Type Grant", FieldType.TEXT, false, 1)]
        public string TypeGrant { get; set; } = string.Empty;
    }

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

    public class PermissionEntry: PermissionBaseModel
    {
        public PermissionEntry()
        {
            
        }

        public PermissionEntry(PermissionResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }


    }

    public class PermissionList : PermissionBaseModel
    {
        public int SaveStatus { get; set; }
    }

    public class PermissionResult : PermissionBaseModel
    {

        public string RoleName { get; set; } = string.Empty;
   
        public string UserName { get; set; } = string.Empty;

        public string ObjectCode { get; set; }

        public string ObjectName { get; set; }
    }
  

}
