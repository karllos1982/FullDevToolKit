﻿using FullDevToolKit.Helpers;
using System.Collections.Generic;

namespace FullDevToolKit.System.Data.QueryBuilders
{
    public class UserRolesQueryBuilder : QueryBuilder
    {
        public UserRolesQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {

            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("UserRoleID");
            ExcludeFields.Add("RecordState");
            ExcludeFields.Add("RoleName");

        }

        public override string QueryForGet(object param)
        {
            string ret = @"Select * from sysUserRoles
                where UserRoleID=@pUserRoleID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select a.UserRoleID, a.RoleID, r.RoleName             
             from sysUserRoles a
             inner join sysRole r on r.RoleID=a.RoleID
             inner join sysUser u on u.UserID=a.UserID
             where 1=1 
             and (@pUserID=0 or a.UserID=@pUserID)
             and (@pRoleID=0 or a.RoleID=@pRoleID)
             ";

            return ret;
        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select a.UserRoleID, a.UserID, u.UserName, a.RoleID, r.RoleName             
             from sysUserRoles a
             inner join sysRole r on r.RoleID=a.RoleID
             inner join sysUser u on u.UserID=a.UserID
             where 1=1 
             and (@pUserID=0 or a.UserID=@pUserID)
             and (@pRoleID=0 or a.RoleID=@pRoleID)
             ";

            return ret;

        }
    }
}
