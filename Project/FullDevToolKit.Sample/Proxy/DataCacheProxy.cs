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

        public async Task<APIResponse<List<TipoOperacaoValueModel>?>> ListTipoOperacao()
        {
            APIResponse<List<TipoOperacaoValueModel>?> ret = null;

            ret = await this.GetAsJSON<List<TipoOperacaoValueModel>?>("listtipoperacao", null);

            return ret;
        }

       
        public async Task<APIResponse<List<TabelasValueModel>?>> ListTabelas()
        {
            APIResponse<List<TabelasValueModel>?> ret = null;

            ret = await this.GetAsJSON<List<TabelasValueModel>?>("listtabelas",null);

            return ret;
        }

        public async Task<APIResponse<List<RoleList>?>> ListRoles()
        {
            APIResponse<List<RoleList>?> ret = null;

            ret = await this.GetAsJSON<List<RoleList>?>("listroles", null);                

            return ret;
        }

        public async Task<APIResponse<LocalizationTextList>?> ListLanguages()
        {
            APIResponse<LocalizationTextList?> ret = null;

            ret = await this.GetAsJSON<LocalizationTextList?>("listlangs", null);

            return ret;
        }

        public async Task<APIResponse<List<LocalizationTextResult>?>> ListLocalizationTexts()
        {
            APIResponse<List<LocalizationTextResult>?> ret = null;

            ret = await this.GetAsJSON<List<LocalizationTextResult>?>("listlocalizationtexts", null);

            return ret;
        }

    }

}
