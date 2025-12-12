using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using MyApp.Models;
using FullDevToolKit.Core.Common;

namespace Template.Controllers
{
    [Route("myapp/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : APIControllerBase
    {

        public PersonController(IContext context)
        {
            Init(context, "PERSON");
           
        }

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(PersonParam param)
        {
            await ExecuteForRead(param,async (param)=>
            {
                PagedList<PersonResult> data 
                    = await Manager.MainBusinessModule.DomainSet.Person.Search(param);
                ret = SetReturn(data);
            });   
            
            return ret; 
        }
     

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(PersonParam param)
        {
            await ExecuteForRead(param, async(param)=>
            {
                List<PersonList> data
                     = await Manager.MainBusinessModule.DomainSet.Person.List(param);
                ret = SetReturn(data);
            });

            return ret;
        }
      

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            await ExecuteForRead(id, async(param)=>
            {
                PersonResult data 
                   = await Manager.MainBusinessModule
                    .DomainSet.Person.Get(new PersonParam() { pPersonID = long.Parse(id) });
                ret = SetReturn(data);
            })
                ;
            return ret;
        }
      

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(PersonEntry param)
        {
            await ExecuteForSave(param, async(param)=>
            {
                PersonEntry data 
                    = await Manager.MainBusinessModule
                        .DomainSet.Person.Set(param, this.UserID);
                ret = SetReturn(data);
            });

            return ret;         
        }
       

        [HttpPost]
        [Route("contactsentryvalidation")]
        [Authorize]
        public object ContactEntryValidation(PersonContactEntry param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                PersonContactEntry data = null;
                data =  Manager.MainBusinessModule
                    .DomainSet.Person.ContactEntryValidation(param);
                ret = SetReturn<PersonContactEntry>(data);

            }
            else
            {
                ret = SetReturn<PersonContactEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();

            return ret;
        }

    }
}
