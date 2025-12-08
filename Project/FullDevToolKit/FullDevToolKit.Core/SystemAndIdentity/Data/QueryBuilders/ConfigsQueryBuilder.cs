using FullDevToolKit.Helpers;
using System.Collections.Generic;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Data.QueryBuilders
{
    public class ConfigsQueryBuilder : QueryBuilder
    {
        public ConfigsQueryBuilder()
        {
            Initialize();
        }

        public override void Initialize()
        {
            Keys = new List<string>();
            ExcludeFields = new List<string>();

            Keys.Add("ConfigID");

        }

        public override string QueryForGet(object param)
        {
            string ret = @"select * from sysConfigs 
                        where ConfigID=@pConfigID ";


            return ret;
        }

        public override string QueryForList(object param)
        {
            string ret = @"select * from sysConfigs 
                         where 1=1 
                         and (@pConfigID=0 or ConfigID=@pConfigID)
                         and (@pConfigName='' or ConfigName=@pConfigName)                        
                        ";

            return ret;
        }

        public override string GetWhereClausule(object param )
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
                         from sysConfigs p                          
                         where 1=1 
                         and (@pConfigID=0 or p.ConfigID=@pConfigID)                         
                         and (@pConfigName='' or p.ConfigName like '%' + @pConfigName + '%')                         
                        ";

            return ret;

        }

    }
}
