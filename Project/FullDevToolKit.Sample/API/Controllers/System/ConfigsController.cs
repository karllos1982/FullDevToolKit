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
    public class ConfigsController : APIControllerBase
    {
        public ConfigsController(IContext context)
        {
            Init(context, "SYSCONFIGS");
        }


        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(ConfigsParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ConfigsResult> data
                    = await Manager.IdentityModule.Domainset.Configs.Search(param);
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(ConfigsParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ConfigsList> data
                    = await Manager.IdentityModule.Domainset.Configs.List(param);
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
                ConfigsResult data
                    = await Manager.IdentityModule
                          .Domainset.Configs.Get(new ConfigsParam() { pConfigID = Int64.Parse(id) });
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(ConfigsEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                ConfigsEntry data
                    = await Manager.IdentityModule
                         .Domainset.Configs.Set(param, this.UserID);
                ret = SetReturn(data);
            });

            return ret;
        }

    }
}
