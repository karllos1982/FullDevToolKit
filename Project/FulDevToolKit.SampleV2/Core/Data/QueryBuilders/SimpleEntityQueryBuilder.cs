using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace MyApp.Data.QueryBuilders
{
    public class SimpleEntityQueryBuilder : QueryBuilder
    {
        public SimpleEntityQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("SimpleEntityID");
            ExcludeFields = QueryBuilder.GetDefaultExcludesFields();
        }

        public override string QueryForGet(object param)
        {
            string ret =
                "select * from SimpleEntity where SimpleEntityID=@pSimpleEntityID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret =
             @"select * from SimpleEntity
                where 1=1
                and (@pSimpleEntityID=0 or SimpleEntityID=@pSimpleEntityID) 
                and (@pSimpleEntityName='' or SimpleEntityName=@pSimpleEntityName)
                order by SimpleEntityName asc        
                ";

            return ret;
        }

        public override string GetWhereClausule(object param)
        {
            string ret = "";

            ret = @" where 1=1 
                 and (@pSimpleEntityName='' or s.SimpleEntityName like '%' + @pSimpleEntityName + '%')
                 and (@pSimpleEntityDescription='' or s.SimpleEntityDescription like '%' + @pSimpleEntityDescription + '%')
                 ";

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
             from SimpleEntity s             
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;
        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select *           
             from SimpleEntity s             
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;
        }
    }
}