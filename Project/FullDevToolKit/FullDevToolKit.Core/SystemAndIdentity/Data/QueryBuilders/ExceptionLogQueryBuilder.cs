using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class ExceptionLogQueryBuilder : QueryBuilder
    {
        public ExceptionLogQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("ExceptionLogID");

        }

        public override string QueryForGet(object param)
        {
            string ret = "";

            ret = @"select e.*, u.Email
                   from sysExceptionLog e 
                   left join sysUser u on e.UserID=u.UserID
                   where ExceptionLogID=@pExceptionLogID"; 
                            
            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("sysExceptionLog", "l", true, "", "", JOINTYPE.NONE, null);

            ret = SelectBuilder.BuildQuery();

            return ret;
        }


        public override string GetWhereClausule(object param)
        {
            string ret = "";

            bool gobydate = ((ExceptionLogParam)param).SearchByDate;

            ret = @" where 1=1 
                 and (@pOrigin='' or s.Origin like '%' + @pOrigin + '%')
                 ";

            if (gobydate)
            {
                ret = ret + " and (s.Date between @pDate_Start and @pData_End )";
            }

            BaseParam b_param = ((BaseParam)param);
            if (b_param.Pagination != null)
            {
                ret = ret + " and " + this.BuildWhereClausuleForPaging("s", b_param.Pagination);
            }


            return ret;
        }

        public override string QueryForPaginationSettings(object param)
        {

            string ret = @"select s.Seq             
             from sysExceptionLog s
             left join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;

        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select u.UserName, u.Email, s.*             
             from sysExceptionLog s
             left join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

           
            return ret;

        }
   

    }
}
