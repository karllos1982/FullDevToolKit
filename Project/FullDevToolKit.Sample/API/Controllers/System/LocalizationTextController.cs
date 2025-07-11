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
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<LocalizationTextResult> data = null;
                data = await Manager.IdentityModule.Domainset.LocalizationText.Search(param);
                ret = SetReturn<List<LocalizationTextResult>>(data);

            }
            else
            {
                ret = SetReturn<List<LocalizationTextResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
         
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(LocalizationTextParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<LocalizationTextList> data = null;
                data = await Manager.IdentityModule.Domainset.LocalizationText.List(param);
                ret = SetReturn<List<LocalizationTextList>>(data);

            }
            else
            {
                ret = SetReturn<List<LocalizationTextList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
          
            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                LocalizationTextResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.LocalizationText.Get(new LocalizationTextParam() { pLocalizationTextID = Int64.Parse(id) });

                ret = SetReturn<LocalizationTextResult>(data);
            }
            else
            {
                ret = SetReturn<LocalizationTextResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
        
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(LocalizationTextEntry param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);


            if (IsAllowed)
            {
                LocalizationTextEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.LocalizationText.Set(param, this.UserID);
                ret = SetReturn<LocalizationTextEntry>(data);

            }
            else
            {
                ret = SetReturn<LocalizationTextResult>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
       
            return ret;
        }

    }
}
