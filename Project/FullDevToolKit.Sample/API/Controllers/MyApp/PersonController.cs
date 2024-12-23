using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using MyApp.Models;
using MyApp.Contracts.Domains;
using MyApp.Contracts.Managers;
using MyApp.Managers;
using System.Collections.Generic;

namespace Template.Controllers
{
    [Route("myapp/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : APIControllerBase
    {

        public PersonController(IContext context,
                IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "Person");
           
        }

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(PersonParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);            

            if (IsAllowed)
            {
                List<PersonResult> data = null;
                data = await Manager.MainBusinessModule.DomainSet.Person.Search(param);                
                ret = SetReturn<List<PersonResult>>(data);
                
            }
            else
            {
                ret = SetReturn<List<PersonResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret; 

        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(PersonParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);            

            if (IsAllowed)
            {
                List<PersonList> data = null;
                data = await Manager.MainBusinessModule.DomainSet.Person.List(param);
                ret = SetReturn<List<PersonList>>(data);

            }
            else
            {
                ret = SetReturn<List<PersonList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                PersonResult data = null;
                data = await Manager.MainBusinessModule
                    .DomainSet.Person.Get(new PersonParam() { pPersonID = long.Parse(id) });
                ret = SetReturn<PersonResult>(data);

            }
            else
            {
                ret = SetReturn<PersonResult> (PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(PersonEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            
            if (IsAllowed)
            {
                PersonEntry data = null;
                data = await Manager.MainBusinessModule
                    .DomainSet.Person.Set(param, this.UserID);
                ret = SetReturn<PersonEntry>(data);

            }
            else
            {
                ret = SetReturn<PersonEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();              
           
            return ret;
        }

        [HttpPost]
        [Route("contactsentryvalidation")]
        [Authorize]
        public object ContactEntryValidation(PersonContactEntry param)
        {
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
