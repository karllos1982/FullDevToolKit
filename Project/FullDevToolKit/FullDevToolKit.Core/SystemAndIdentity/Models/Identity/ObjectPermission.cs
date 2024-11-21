using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{
    public class ObjectPermissionParam
    {
        public ObjectPermissionParam()
        {
            pObjectCode = "";
            pObjectName = "";
            pObjectPermissionID = 0; 
        }

        public long pObjectPermissionID { get; set; }

        public string pObjectName { get; set; }

        public string pObjectCode{ get; set; }

    }

    public class ObjectPermissionEntry
    {
        public ObjectPermissionEntry()
        {
            
        }

        public ObjectPermissionEntry(ObjectPermissionResult fromobj)
        {
            ObjectPermissionID = fromobj.ObjectPermissionID;
            ObjectName = fromobj.ObjectName;    
            ObjectCode = fromobj.ObjectCode;    
        }

        public long ObjectPermissionID { get; set; }

        [PrimaryValidationConfig("ObjectName", "Object Name", FieldType.TEXT, false, 50)]
        public string ObjectName { get; set; } = string.Empty;

        [PrimaryValidationConfig("ObjectCode", "Object Code", FieldType.TEXT, false, 25)]
        public string ObjectCode { get; set; } = string.Empty;

    }

    public class ObjectPermissionList
    {
        public long ObjectPermissionID { get; set; }

        public string ObjectName { get; set; } = string.Empty;

        public string ObjectCode { get; set; } = string.Empty;

    }

    public class ObjectPermissionResult
    {
        public long ObjectPermissionID { get; set; }

        public string ObjectName { get; set; } = string.Empty;

        public string ObjectCode { get; set; } = string.Empty;

    }

}
