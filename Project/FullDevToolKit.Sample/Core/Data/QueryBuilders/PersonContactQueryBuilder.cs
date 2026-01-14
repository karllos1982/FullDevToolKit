using FullDevToolKit.Helpers;

namespace MyApp.Data.QueryBuilders
{
    public class PersonContactQueryBuilder : QueryBuilder
    {
        public PersonContactQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("PersonContactID");
            ExcludeFields = QueryBuilder.GetDefaultExcludesFields();
            ExcludeFields.Add("RecordState"); 

        }

        public override string QueryForGet(object param)
        {
            string ret =
                @"select * from PersonContacts where PersonContactID=@pPersonContactID";
          
            return ret;
        }

        public override string QueryForList(object param)
        {

            string ret =
              @"select * from PersonContacts 
                where (@pPersonContactID=0 or PersonContactID=@pPersonContactID)
                and (@pPersonID=0 or PersonID=@pPersonID) 
                and (@pContactName='' or ContactName=@pContactName) 
                and (@pEmail='' or Email=@pEmail)
                order by ContactName asc        
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

            string ret =
                @"select * from PersonContacts 
                where (@pPersonContactID=0 or PersonContactID=@pPersonContactID)
                and (@pPersonID=0 or PersonID=@pPersonID) 
                and (@pContactName='' or ContactName=@pContactName) 
                and (@pEmail='' or Email=@pEmail)
                order by ContactName asc        
                ";

            return ret;

        }


    }
}
