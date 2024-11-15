﻿using FullDevToolKit.Common;

namespace FullDevToolKit.System.Models.Identity
{
    public class UserInstancesParam
    {

        public UserInstancesParam()
        {
            pUserInstanceID = 0;
            pUserID = 0;
            pInstanceID = 0;
        }

        public Int64 pUserInstanceID { get; set; }

        public Int64 pUserID { get; set; }

        public Int64 pInstanceID { get; set; }
    }

    public class UserInstancesEntry
    {
        public Int64 UserInstanceID { get; set; }

        public Int64 UserID { get; set; }

        public Int64 InstanceID { get; set; }      

        public RECORDSTATEENUM RecordState { get; set; }
    }


    public class UserInstancesList
    {
        public Int64 UserInstanceID { get; set; }

        public Int64 InstanceID { get; set; }

        public string InstanceName { get; set; }

    }

    public class UserInstancesResult
    {
        public Int64 UserInstanceID { get; set; }

        public Int64 UserID { get; set; }

        public string UserName { get; set; }

        public Int64 InstanceID { get; set; }

        public string InstanceName { get; set; }
        

    }

}
