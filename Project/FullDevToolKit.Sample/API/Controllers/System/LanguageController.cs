using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class LanguageController : APIControllerBase
    {
        public LanguageController(IContext context)
        {
            Init(context, "SYSLANGUAGE");
        }


        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(LanguageParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<LanguageResult> data
                    = await Manager.IdentityModule.Domainset.Language.Search(param);
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(LanguageParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<LanguageList> data
                    = await Manager.IdentityModule.Domainset.Language.List(param);
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            await ExecuteForRead(id, async (param) =>
            {
                LanguageResult data
                    = await Manager.IdentityModule
                          .Domainset.Language.Get(new LanguageParam() { pLanguageID = Int64.Parse(id) });
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(LanguageEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                LanguageEntry data
                    = await Manager.IdentityModule
                         .Domainset.Language.Set(param, this.UserID);
                ret = SetReturn(data);
            });

            return ret;
        }

    }
}
