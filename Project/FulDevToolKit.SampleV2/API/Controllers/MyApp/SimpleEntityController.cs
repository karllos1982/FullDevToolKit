using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using MyApp.Models;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;

namespace API.Controllers
{
    [Route("myapp/[controller]")]
    [ApiController]
    [Authorize]
    public class SimpleEntityController : APIControllerBase
    {
        public SimpleEntityController(IContext context)
        {
            Init(context, "SIMPLEENTITY");
        }

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(SimpleEntityParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                PagedList<SimpleEntityResult> data
                    = await Manager.MainBusinessModule.DomainSet.SimpleEntity.Search(param);
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(SimpleEntityParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<SimpleEntityList> data
                     = await Manager.MainBusinessModule.DomainSet.SimpleEntity.List(param);
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
                SimpleEntityResult data
                   = await Manager.MainBusinessModule
                    .DomainSet.SimpleEntity.Get(new SimpleEntityParam() { pSimpleEntityID = long.Parse(id) });
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(SimpleEntityEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                SimpleEntityEntry data
                    = await Manager.MainBusinessModule
                        .DomainSet.SimpleEntity.Set(param, this.UserID);
                ret = SetReturn(data);
            });

            return ret;
        }

        [HttpPost]
        [Route("remove")]
        [Authorize]
        public async Task<object> Remove(SimpleEntityEntry param)
        {
            await ExecuteForDelete(param, async (param) =>
            {
                SimpleEntityEntry data
                    = await Manager.MainBusinessModule
                        .DomainSet.SimpleEntity.Remove(param, this.UserID);
                ret = SetReturn(data);
            });

            return ret;
        }
    }
}