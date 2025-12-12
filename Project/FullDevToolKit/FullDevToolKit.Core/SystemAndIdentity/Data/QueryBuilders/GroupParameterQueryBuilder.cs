using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class GroupParameterQueryBuilder : QueryBuilder
    {
        public GroupParameterQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("GroupParameterID");

        }

        public override string QueryForGet(object param)
        {
            string ret = @"select * from sysGroupParameter 
                        where GroupParameterID=@pGroupParameterID ";

                        
            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select * from sysGroupParameter 
                         where 1=1 
                         and (@pGroupParameterID=0 or GroupParameterID=@pGroupParameterID)
                         and (@pGroupParameterName='' or GroupParameterName=@pGroupParameterName)                         
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
            string ret = @"select * from sysGroupParameter 
                         where 1=1 
                         and (@pGroupParameterID=0 or GroupParameterID=@pGroupParameterID)
                         and (@pGroupParameterName='' or GroupParameterName like '%' + @pGroupParameterName + '%')                         
                        ";

            return ret;

        }        

    }
}
