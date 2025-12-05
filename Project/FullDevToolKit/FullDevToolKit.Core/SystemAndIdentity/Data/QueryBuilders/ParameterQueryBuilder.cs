using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class ParameterQueryBuilder : QueryBuilder
    {
        public ParameterQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("ParameterID");

        }

        public override string QueryForGet(object param)
        {
            string ret = @"select * from sysParameter 
                        where ParameterID=@pParameterID ";


            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select * from sysParameter 
                         where 1=1 
                         and (@pParameterID=0 or ParameterID=@pParameterID)
                         and (@pParameterName='' or ParameterName=@pParameterName)                        
                        ";

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
            string ret = @"select p.*, g.GroupParameterName
                         from sysParameter p 
                         inner join sysGroupParameter g on p.GroupParameterID=g.GroupParameterID
                         where 1=1 
                         and (@pParameterID=0 or p.ParameterID=@pParameterID)
                         and (@pGroupParameterID=0 or p.GroupParameterID=@pGroupParameterID)
                         and (@pParameterName='' or p.ParameterName like '%' + @pParameterName + '%')                         
                        ";

            return ret;

        }

    }
}
