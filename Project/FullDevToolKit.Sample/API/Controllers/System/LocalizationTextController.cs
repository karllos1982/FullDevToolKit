using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class LocalizationTextController : APIControllerBase
    {
        public LocalizationTextController(IContext context)
        {
            Init(context, "SYSLOCALIZATIONTEXT");
        }

      
        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(LocalizationTextParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<LocalizationTextResult> data
                    = await Manager.IdentityModule.Domainset.LocalizationText.Search(param);
                ret = SetReturn(data);
            });
                  
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(LocalizationTextParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<LocalizationTextList> data
                    = await Manager.IdentityModule.Domainset.LocalizationText.List(param);
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
                LocalizationTextResult data
                    = await Manager.IdentityModule
                         .Domainset.LocalizationText.Get(new LocalizationTextParam() { pLocalizationTextID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
                  
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(LocalizationTextEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                LocalizationTextEntry data 
                    = await Manager.IdentityModule
                        .Domainset.LocalizationText.Set(param, this.UserID);
                ret = SetReturn(data);
            });
                   
            return ret;
        }

    }
}
