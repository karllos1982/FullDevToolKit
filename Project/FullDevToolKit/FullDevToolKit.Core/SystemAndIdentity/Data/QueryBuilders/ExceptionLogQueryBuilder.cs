using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common; 

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

            return ret;
        }

        public override string QueryForPaginationSettings(object param)
        {

            string ret = "";

            return ret;

        }

        public override string QueryForSearch(object param)
        {
            bool gobydate = ((ExceptionLogParam)param).SearchByDate;

            string ret = "";

            ret = @"select e.*, u.Email
                   from sysExceptionLog e 
                   left join sysUser u on e.UserID=u.UserID
                   where 1=1
                   and (@pOrigin='' or e.Origin=@pOrigin)
                    ";
            
            if (gobydate)
            {
                ret = ret + " and (e.Date between @pDate_Start and @pData_End )";
            }

            ret = ret + " order by e.Date desc"; 

            return ret;

        }
   

    }
}
