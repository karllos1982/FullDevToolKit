using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.API;


namespace MyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DataCacheController : APIControllerBase
    {
       
        public DataCacheController(
            IContext context,                       
            IWebHostEnvironment hostingEnvironment,
            IMemoryCache _cache)
        {
            memorycache = _cache;
            Init(context, null, "");
        }

        private string GetText(List<LocalizationTextItem> texts, string code)
        {
            string ret = code;

            LocalizationTextItem item
                = texts.Where(t => t.Name == code).FirstOrDefault(); 

            if (item != null)
            {
                ret = item.Text;
            }

            return ret;
        }

        [HttpGet]
        [Route("listtipoperacao")]
        public async Task<object> ListTipoOperacao()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<TipoOperacaoValueModel> list = null;

            list = memorycache.Get<List<TipoOperacaoValueModel>>("TIPOOPERACAO");

            if (list == null)
            {
                List<LocalizationTextItem> texts
                    = await Manager.Context.GetLocalizationTextsAsync();  
               
                list = new List<TipoOperacaoValueModel>()
                    {
                        new TipoOperacaoValueModel(){ Value="I", Text=GetText(texts,"InsertOperation-Text") },
                        new TipoOperacaoValueModel(){ Value="U", Text=GetText(texts,"UpdateOperation-Text")},
                        new TipoOperacaoValueModel(){ Value="D", Text=GetText(texts,"DeleteOperation-Text")}
                    };
            
                memorycache.Set("TIPOOPERACAO", list, this.GetMemoryCacheOptionsByHour(2)); 
            }

            ret = SetReturn<List<TipoOperacaoValueModel>>(list);

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listtabelas")]
        public async Task<object> ListTabelas()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<TabelasValueModel> list = null;

            list = memorycache.Get<List<TabelasValueModel>>("TABELAS");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset.DataLog.GetTableList();               
                
                memorycache.Set("TABELAS", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = SetReturn<List<TabelasValueModel>>(list);

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listroles")]
        public async Task<object> ListRoles()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<RoleList> list = null;

            list = memorycache.Get<List<RoleList>>("ROLES");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset.Role.List(new RoleParam());

                memorycache.Set("ROLES", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = SetReturn<List<RoleList>>(list);

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listlangs")]
        public async Task<object> ListLanguages()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<LocalizationTextList> list = null;

            list = memorycache.Get<List<LocalizationTextList>>("LANGS");

            if (list == null)
            {
                //list = await Manager.IdentityModule.Domainset.LocalizationText.GetListOfLanguages();
                list = new List<LocalizationTextList>();
                list.Add(new LocalizationTextList() { Language = "en-us" });
                list.Add(new LocalizationTextList() { Language = "pt-br" });

                memorycache.Set("LANGS", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = SetReturn<List<LocalizationTextList>>(list);

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listlocalizationtexts")]
		
		public async Task<object> ListLocalizationTexts()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<LocalizationTextResult> list = null;

            list = memorycache.Get<List<LocalizationTextResult>>("LOCALIZATIONTEXTS");

            if (list == null)
            {				
				list = await Manager.IdentityModule.Domainset
                    .LocalizationText.Search(new LocalizationTextParam()); 

                memorycache.Set("LOCALIZATIONTEXTS", list, this.GetMemoryCacheOptionsByHour(2));

            }

			ret = SetReturn<List<LocalizationTextResult>>(list);
			
            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listgroupparameter")]
        public async Task<object> ListGroupParameter()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<GroupParameterResult> list = null;

            list = memorycache.Get<List<GroupParameterResult>>("GROUPPARAMETER");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset
                    .GroupParameter.Search(new GroupParameterParam());

                memorycache.Set("GROUPPARAMETER", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = SetReturn<List<GroupParameterResult>>(list);

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listparameter")]
        public async Task<object> ListParameter()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<ParameterResult> list = null;

            list = memorycache.Get<List<ParameterResult>>("PARAMETER");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset
                    .Parameter.Search(new ParameterParam());

                memorycache.Set("PARAMETER", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = SetReturn<List<ParameterResult>>(list);

            FinalizeManager();

            return ret;
        }


    }
}
