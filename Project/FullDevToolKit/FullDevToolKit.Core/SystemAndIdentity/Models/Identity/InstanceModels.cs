using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class InstanceParam
    {
        public InstanceParam()
        {        
            pInstanceID = 0;
            pInstanceName = "";
            pInstanceTypeName = "";
        }


        public long pInstanceID { get; set; }
        
        public string pInstanceTypeName { get; set; }

        public string pInstanceName { get; set; }

        
    }

    public class InstanceEntry
    {

        public InstanceEntry()
        {
            
        }

        public InstanceEntry(InstanceResult fromobj)
        {
            InstanceID = fromobj.InstanceID;
            InstanceName = fromobj.InstanceName;
            InstanceTypeName = fromobj.InstanceTypeName;
            IsActive = fromobj.IsActive; 
            CreateDate = fromobj.CreateDate;
        }


        [PrimaryValidationConfig("InstanceID", "LocalizationText ID", FieldType.NUMERIC, false, 0)]
        public long InstanceID { get; set; }

        [PrimaryValidationConfig("InstanceTypeName", "LocalizationText Type Name", FieldType.TEXT , false, 50)]
        public string InstanceTypeName { get; set; } = string.Empty;

        [PrimaryValidationConfig("InstanceName", "LocalizationText Name", FieldType.TEXT, false, 100)]
        public string InstanceName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }


    public class InstanceList
    {        
        public long InstanceID { get; set; }
     
        public string InstanceName { get; set; } = string.Empty;    

    }

    public class InstanceResult
    {
        
        public long InstanceID { get; set; }
        
        public string InstanceTypeName { get; set; } = string.Empty;
       
        public string InstanceName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }

}
