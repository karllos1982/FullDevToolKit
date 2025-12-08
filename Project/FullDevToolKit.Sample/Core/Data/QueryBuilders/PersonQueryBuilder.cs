using FullDevToolKit.Helpers;

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

            string ret = ""; 

            SelectBuilder.Clear();
            SelectBuilder.AddTable("Person", "c", true, "PersonID", "", JOINTYPE.NONE, null);                        
            SelectBuilder.AddField("c", "PersonID", "@pPersonID", false, "0", null, ORDERBYTYPE.ASC);

            ret = SelectBuilder.BuildQuery();

            return ret;
        }

        public override string QueryForList(object param)
        {

            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("Person", "c", false, "PersonID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddField("c", "PersonID", "@pPersonID", true, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("c", "PersonName", "@pPersonName", true, "''", null, ORDERBYTYPE.ASC);
            SelectBuilder.AddField("c", "Email", "@pEmail", false, "''", null, ORDERBYTYPE.NONE);

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

            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("Person", "c", true, "PersonID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddField("c", "PersonID", "@pPersonID", false, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("c", "PersonName", "@pPersonName", false, "''", null, ORDERBYTYPE.ASC);
            SelectBuilder.AddField("c", "Email", "@pEmail", false, "''", null, ORDERBYTYPE.NONE);

            ret = SelectBuilder.BuildQuery();

            return ret;

        }


    }
}
