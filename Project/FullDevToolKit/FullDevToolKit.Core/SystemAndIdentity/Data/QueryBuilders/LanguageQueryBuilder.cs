using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class LanguageQueryBuilder : QueryBuilder
    {
        public LanguageQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("LanguageID");

        }

        public override string QueryForGet(object param)
        {
            string ret = @"select * from sysLanguage 
                        where LanguageID=@pLanguageID ";


            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select * from sysLanguage p
                         where 1=1 
                         and (@pLanguageID=0 or LanguageID=@pLanguageID)
                         and (@pLanguageName='' or p.LanguageName like '%' + @pLanguageName + '%')                
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
            string ret = @"select *
                         from sysLanguage p                         
                         where 1=1 
                         and (@pLanguageID=0 or p.LanguageID=@pLanguageID)                         
                         and (@pLanguageName='' or p.LanguageName like '%' + @pLanguageName + '%')                         
                        ";

            return ret;

        }

    }
}
