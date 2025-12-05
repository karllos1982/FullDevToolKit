using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Models.Identity;
using System.Collections.Generic;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class SessionLogQueryBuilder : QueryBuilder
    {
        public SessionLogQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("SessionLogID");
           
        }

        public override string QueryForGet(object param)
        {
            string ret = @"select u.UserName, u.Email, s.*   
                from sysSessionLog
                inner join sysUser u on s.UserID = u.UserID   
                where SessionLogID=@pSessionLogID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select 
             s.SessionLogID, s.UserID, u.UserName, Date, IP, BrowserName
             from sysSessionLog s
             inner join sysUser u on s.UserID = u.UserID               
             ";

            return ret;
        }

        public override string GetWhereClausule(object param)
        {
            string ret = "";

            bool gobydate = ((SessionLogParam)param).SearchByDate;

            ret = @" where 1=1 
                 and (@pUserID=0 or s.UserID=@pUserID)
                 and (@pEmail='' or u.Email=@pEmail)
                 ";

            if (gobydate)
            {
                ret = ret + " and (s.Date between @pDate_Start and @pData_End )";
            }
                     
            return ret;
        }

        public override string QueryForPaginationSettings(object param)
        {

            string ret = @"select count(s.SessionLogID) cnt             
             from sysSessionLog s
             inner join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret + GetWhereClausule(param);

            return ret;

        }

        public override string QueryForSearch(object param)
        {            

            string ret = @"select u.UserName, u.Email, s.*             
             from sysSessionLog s
             inner join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret +  GetWhereClausule(param) + " order by s.Seq";

            
            return ret;

        }

        public string QueryForSetDateLogout()
        {
            string ret = @"update sysSessionLog set DateLogout = GetDate() 
                    where SessionLogID = (select top 1 SessionLogID 
                    from sysSessionLog where UserID=@pUserID order by Date desc)
             ";

            return ret;
        }
    }
}
