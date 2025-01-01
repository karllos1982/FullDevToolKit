using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.API;
using FullDevToolKit.ApplicationHelpers;


namespace MyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DataCacheController : APIControllerBase
    {
        public DataCacheController(
            IContext context,
            IContextBuilder contextbuilder,             
            IWebHostEnvironment hostingEnvironment,
            IMemoryCache _cache)
        {
            Init(context, contextbuilder, "");
            Context.LocalizationLanguage = Context.Settings.LocalizationLanguage;            
            LocalizationText.LoadData(context);
        }

        [HttpGet]
        [Route("listtipoperacao")]
        public object ListTipoOperacao()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<TipoOperacaoValueModel> list = null;

            list = memorycache.Get<List<TipoOperacaoValueModel>>("TIPOOPERACAO");

            if (list == null)
            {
                list = new List<TipoOperacaoValueModel>()
                    {
                        new TipoOperacaoValueModel(){ Value="I", Text="Inserção"},
                        new TipoOperacaoValueModel(){ Value="U", Text="Edição"},
                        new TipoOperacaoValueModel(){ Value="D", Text="Exclusão"}
                    };
            
                memorycache.Set("TIPOOPERACAO", list, this.GetMemoryCacheOptionsByHour(2)); 
            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listtabelas")]
        public async Task<object> ListTabelas()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<TabelasValueModel> list = null;

            list = memorycache.Get<List<TabelasValueModel>>("TABELAS");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset.DataLog.GetTableList();               
                
                memorycache.Set("TABELAS", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listroles")]
        public async Task<object> ListRoles()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<RoleList> list = null;

            list = memorycache.Get<List<RoleList>>("ROLES");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset.Role.List(new RoleParam());

                memorycache.Set("ROLES", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listlangs")]
        public async Task<object> ListLanguages()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<LocalizationTextList> list = null;

            list = memorycache.Get<List<LocalizationTextList>>("LANGS");

            if (list == null)
            {
                //list = await Manager.IdentityModule.Domainset.LocalizationText.GetListOfLanguages();
                list = new List<LocalizationTextList>();
                list.Add(new LocalizationTextList() { Language = "en-US" });
                list.Add(new LocalizationTextList() { Language = "pt-BR" });

                memorycache.Set("LANGS", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listlocalizationtexts")]
        public async Task<object> ListLocalizationTexts()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<LocalizationTextResult> list = null;

            list = memorycache.Get<List<LocalizationTextResult>>("LOCALIZATIONTEXTS");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset
                    .LocalizationText.Search(new LocalizationTextParam()); 

                memorycache.Set("LOCALIZATIONTEXTS", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listgroupparameter")]
        public async Task<object> ListGroupParameter()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<GroupParameterResult> list = null;

            list = memorycache.Get<List<GroupParameterResult>>("GROUPPARAMETER");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset
                    .GroupParameter.Search(new GroupParameterParam());

                memorycache.Set("GROUPPARAMETER", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("listparameter")]
        public async Task<object> ListParameter()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<ParameterResult> list = null;

            list = memorycache.Get<List<ParameterResult>>("PARAMETER");

            if (list == null)
            {
                list = await Manager.IdentityModule.Domainset
                    .Parameter.Search(new ParameterParam());

                memorycache.Set("PARAMETER", list, this.GetMemoryCacheOptionsByHour(2));

            }

            ret = list;

            FinalizeManager();

            return ret;
        }


    }
}
