﻿using FullDevToolKit.Helpers;
using System.Collections.Generic;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class RoleQueryBuilder : QueryBuilder
    {
        public RoleQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("RoleID"); 
        }

        public override string QueryForGet(object param)
        {
            string ret = "Select * from sysRole where RoleID=@pRoleID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select *, cast(RoleID as varchar(32)) as sRoleID              
             from sysRole 
             where 1=1 
             and (@pRoleName='' or RoleName=@pRoleName)
             and (@pRoleID=0 or RoleID=@pRoleID)
             ";

            return ret;
        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select *             
             from sysRole 
             where 1=1 
             and (@pRoleName='' or RoleName=@pRoleName)
             and (@pRoleID=0 or RoleID=@pRoleID)
             ";

            return ret;

        }
    }
}
