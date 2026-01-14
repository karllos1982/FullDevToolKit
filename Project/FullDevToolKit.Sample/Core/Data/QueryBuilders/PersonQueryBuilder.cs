using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Models.Identity;

namespace MyApp.Data.QueryBuilders
{
    public class PersonQueryBuilder : QueryBuilder
    {
        public PersonQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("PersonID");
            ExcludeFields = QueryBuilder.GetDefaultExcludesFields(); 
            ExcludeFields.Add("Contacts"); 

        }

        public override string QueryForGet(object param)
        {

            string ret =
                "select * from Person where PersonID=@pPersonID";       

            return ret;
        }

        public override string QueryForList(object param)
        {

            string ret =
             @"select * from Person                 
                and (@pPersonID=0 or PersonID=@pPersonID) 
                and (@pPersonName='' or PersonName=@pPersonName) 
                and (@pEmail='' or Email=@pEmail)
                order by PersonName asc        
                ";         

            return ret;
        }

        public override string GetWhereClausule(object param)
        {
            string ret = "";
                        
            ret = @" where 1=1 
                 and (@pPersonName='' or s.PersonName like '%' + @pPersonName + '%')
                 and (@pEmail='' or s.Email like '%' + @pEmail + '%')
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
             from Person s             
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;

        }
        public override string QueryForSearch(object param)
        {

            string ret = @"select *           
             from Person s             
             ";

            ret = ret + GetWhereClausule(param) + " order by s.Seq";

            return ret;

        }


    }
}
