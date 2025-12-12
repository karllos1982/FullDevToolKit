using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using System.Collections.Generic;


namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class DataLogQueryBuilder : QueryBuilder
    {
        public DataLogQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("DataLogID");
            
        }

        public override string QueryForGet(object param)
        {
            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("sysDataLog", "l", true, "UserID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddTable("sysUser", "u", false, "DataLogID", "UserID", JOINTYPE.INNER, "l");
            SelectBuilder.AddField("u", "Email", "", true,null,null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("l", "DataLogID", "@pDataLogID",false,"0",null, ORDERBYTYPE.NONE);
            
            ret = SelectBuilder.BuildQuery();

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = ""; 

            SelectBuilder.Clear();
            SelectBuilder.AddTable("sysDataLog", "l", true, "", "", JOINTYPE.NONE, null);

            ret = SelectBuilder.BuildQuery();

            return ret;
        }

        public override string GetWhereClausule(object param)
        {
            string ret = "";

            bool gobydate = ((DataLogParam)param).SearchByDate;

            ret = @" where 1=1 
                 and (@pUserID=0 or s.UserID=@pUserID)
                 and (@pEmail='' or u.Email like '%' + @pEmail + '%')
                 and (@pTableName='0' or s.TableName=@pTableName) 
                 and (@pOperation='0' or s.Operation=@pOperation)    
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
             from sysDataLog s
             inner join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;

        }
        public override string QueryForSearch(object param)
        {
             string ret = @"select u.UserName, u.Email, s.*             
             from sysDataLog s
             inner join sysUser u on s.UserID = u.UserID  
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";


            return ret;

        }

        public string QueryForGetTimeLine()
        {
            string ret =
                    @"Select 
                     a.DataLogID, a.Operation , a.Date,
                    u.email as UserEmail                  
                    from sysDataLog a 
                    inner join sysUser u on a.UserID = u.UserID
                    where a.ID=@pID
                    order by a.Date";

            return ret;

        }

        public string QueryForGetTableList()
        {
            string ret =
                    @"select distinct TableName 'Value', TableName 'Text' from sysDataLog order by TableName ";

            return ret;

        }

    }
}
