﻿using FullDevToolKit.Helpers;
using System.Collections.Generic;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class UserInstancesQueryBuilder : QueryBuilder
    {
        public UserInstancesQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {

            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("UserInstanceID");
            ExcludeFields.Add("RecordState");           
        }

        public override string QueryForGet(object param)
        {
            string ret = @"select a.UserInstanceID, a.UserID, u.UserName, a.InstanceID, i.InstanceName  
                where UserInstanceID=@pUserInstanceID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select a.UserInstanceID, a.InstanceID, i.InstanceName             
             from sysUserInstances a 
             inner join sysInstance i on i.InstanceID=a.InstanceID
             inner join sysUser u on u.UserID=a.UserID
             where 1=1 
             and (@pUserID=0 or a.UserID=@pUserID)
             and (@pInstanceID=0 or a.InstanceID=@pInstanceID)
             ";

            return ret;
        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select a.UserInstanceID, a.UserID, u.UserName, a.InstanceID, i.InstanceName             
             from sysUserInstances a 
             inner join sysInstance i on i.InstanceID=a.InstanceID
             inner join sysUser u on u.UserID=a.UserID
             where 1=1 
             and (@pUserID=0 or a.UserID=@pUserID)
             and (@pInstanceID=0 or a.InstanceID=@pInstanceID)
             ";

            return ret;

        }

        public string QueryForAlterInstance(object param)
        {
            string ret = @"update sysuserinstances set instanceid = @pInstanceID
                        where userinstanceid = @pUserInstanceID
                     ";

            return ret;

        }
    }
}
