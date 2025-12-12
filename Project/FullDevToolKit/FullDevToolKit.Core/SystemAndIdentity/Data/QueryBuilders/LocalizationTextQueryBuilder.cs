using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class LocalizationTextQueryBuilder : QueryBuilder
    {
        public LocalizationTextQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("LocalizationTextID");
        }

        public override string QueryForGet(object param)
        {
            string ret = @"Select t.*, l.LanguageName 
            from sysLocalizationText t
            inner join sysLanguage l on t.LanguageID=l.LanguageID
            where t.LocalizationTextID=@pLocalizationTextID";

            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select t.*, l.LanguageName            
             from sysLocalizationText t
             inner join sysLanguage l on t.LanguageID=l.LanguageID
             where 1=1 
             and (@pLanguageID=0 or t.LanguageID=@pLanguageID)
             and (@pName='' or t.Name like '%' + @pName + '%')
             and (@pCode='' or t.Code=@pCode)
             and (@pLocalizationTextID=0 or LocalizationTextID=@pLocalizationTextID)      
             ";

            return ret;
        }

        public override string GetWhereClausule(object param)
        {
            string ret = "";

            ret = @"where 1=1 
             and (@pLanguageID=0 or t.LanguageID=@pLanguageID)
             and (@pName='' or t.Name like '%' + @pName + '%')
             and (@pCode='' or t.Code=@pCode)
             and (@pLocalizationTextID=0 or LocalizationTextID=@pLocalizationTextID) 
            ";
           
            BaseParam b_param = ((BaseParam)param);
            if (b_param.Pagination != null)
            {
                ret = ret + " and " + this.BuildWhereClausuleForPaging("t", b_param.Pagination);
            }

            return ret;
        }

        public override string QueryForPaginationSettings(object param)
        {

            string ret = @"select t.Seq             
             from sysLocalizationText t
             inner join sysLanguage l on t.LanguageID=l.LanguageID  
             ";

            ret = ret + GetWhereClausule(param) + " order by t.Seq";

            return ret;

        }

        public override string QueryForSearch(object param)
        {
            string ret = @"select t.*, l.LanguageName            
             from sysLocalizationText t
             inner join sysLanguage l on t.LanguageID=l.LanguageID                  
             ";          

            ret = ret + GetWhereClausule(param) + " order by t.Seq";

            return ret;

        }

        public  string QueryForListOfLanguages(object param)
        {
            string ret = @"select distinct [language]          
             from syslocalizationtext                 
             ";

            return ret;
        }

    }
}
