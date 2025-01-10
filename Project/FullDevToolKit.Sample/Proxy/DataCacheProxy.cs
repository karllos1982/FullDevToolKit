using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;


namespace MyApp.Proxys
{
    public interface IDataCacheProxyManager
    {
        void Init(HttpClient http, string baseurl, string token);
       
     }

    public class DataCacheProxy : APIProxyBase, IDataCacheProxyManager
    {


        public DataCacheProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            this.InitializeAPI(http, baseurl + "/datacache/", token);

        }      

        public async Task<List<TipoOperacaoValueModel>?> ListTipoOperacao()
        {
            List<TipoOperacaoValueModel> ret = new List<TipoOperacaoValueModel>();
            APIResponse<List<TipoOperacaoValueModel>?> response = null;

            response = await this.GetAsJSON<List<TipoOperacaoValueModel>?>("listtipoperacao", null);

            if ( response.IsSuccess)
            {
                ret = response.Data; 
            }

            return ret;
        }

        public async Task<List<TabelasValueModel>?> ListTabelas()
        {
            List<TabelasValueModel> ret = new List<TabelasValueModel>();
            APIResponse<List<TabelasValueModel>?> response = null;

            response = await this.GetAsJSON<List<TabelasValueModel>?>("listtabelas", null);

            if (response.IsSuccess)
            {
                ret = response.Data;
            }

            return ret;
        }

        public async Task<List<RoleList>?> ListRoles()
        {
            List<RoleList> ret = new List<RoleList>();
            APIResponse<List<RoleList>?> response = null;

            response = await this.GetAsJSON<List<RoleList>?>("listroles", null);

            if (response.IsSuccess)
            {
                ret = response.Data;
            }

            return ret;
        }


        public async Task<List<LocalizationTextList>?> ListLanguages()
        {
            List<LocalizationTextList> ret = new List<LocalizationTextList>();
            APIResponse<List<LocalizationTextList>?> response = null;

            response = await this.GetAsJSON<List<LocalizationTextList>?>("listlangs", null);

            if (response.IsSuccess)
            {
                ret = response.Data;
            }

            return ret;
        }

        //public async Task<List<LocalizationTextResult>?> ListLocalizationTexts()
        //{
        //    List<LocalizationTextResult> ret = new List<LocalizationTextResult>();
        //    APIResponse<List<LocalizationTextResult>?> response = null;

        //    response = await this.GetAsJSON<List<LocalizationTextResult>?>("listlocalizationtexts", null);

        //    if (response.IsSuccess)
        //    {
        //        ret = response.Data;
        //    }

        //    return ret;
        //}


		public async Task<List<LocalizationTextResult>> ListLocalizationTexts()
		{
            List<LocalizationTextResult> ret = null;

			APIResponse<List<LocalizationTextResult>?> response = null;			

			response = await GetAsJSON<List<LocalizationTextResult>?>("listlocalizationtexts", null);

            if (response.IsSuccess)
            {
                ret = response.Data; 
            }

			return ret;
		}


	}

}
