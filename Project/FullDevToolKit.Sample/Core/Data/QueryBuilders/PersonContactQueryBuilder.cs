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
            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("PersonContacts", "s", true, "PersonContactID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddField("s", "PersonContactID", "@pPersonContactID", false, "0", null, ORDERBYTYPE.ASC);

            ret = SelectBuilder.BuildQuery();          

            return ret;
        }

        public override string QueryForList(object param)
        {

            string ret = "";

            SelectBuilder.Clear();
            SelectBuilder.AddTable("PersonContacts", "s", true, "PersonContactID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddField("s", "PersonContactID", "@pPersonContactID", true, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("s", "PersonID", "@pPersonID", true, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("s", "ContactName", "@pContactName", true, "''", null, ORDERBYTYPE.ASC );
            SelectBuilder.AddField("s", "Email", "@pEmail", false, "''", null, ORDERBYTYPE.NONE);

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
            SelectBuilder.AddTable("PersonContacts", "s", true, "PersonContactID", "", JOINTYPE.NONE, null);
            SelectBuilder.AddField("s", "PersonContactID", "@pPersonContactID", false, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("s", "PersonID", "@pPersonID", false, "0", null, ORDERBYTYPE.NONE);
            SelectBuilder.AddField("s", "ContactName", "@pContactName", false, "''", null, ORDERBYTYPE.ASC);
            SelectBuilder.AddField("s", "Email", "@pEmail", false, "''", null, ORDERBYTYPE.NONE);

            ret = SelectBuilder.BuildQuery();          

            return ret;

        }


    }
}
