﻿using FullDevToolKit.Helpers;

namespace FullDevToolKit.System.Models.Identity
{

    public class InstanceParam
    {
        public InstanceParam()
        {        
            pInstanceID = 0;
            pInstanceName = "";
            pInstanceTypeName = "";
        }


        public Int64 pInstanceID { get; set; }
        
        public string pInstanceTypeName { get; set; }

        public string pInstanceName { get; set; }

        
    }

    public class InstanceEntry
    {
        [PrimaryValidationConfig("InstanceID", "LocalizationText ID", FieldType.NUMERIC, false, 0)]
        public Int64 InstanceID { get; set; }

        [PrimaryValidationConfig("InstanceTypeName", "LocalizationText Type Name", FieldType.TEXT , false, 50)]
        public string InstanceTypeName { get; set; }

        [PrimaryValidationConfig("InstanceName", "LocalizationText Name", FieldType.TEXT, false, 100)]
        public string InstanceName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }


    public class InstanceList
    {        
        public Int64 InstanceID { get; set; }
     
        public string InstanceName { get; set; }

    }

    public class InstanceResult
    {
        
        public Int64 InstanceID { get; set; }
        
        public string InstanceTypeName { get; set; }
       
        public string InstanceName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }

}
