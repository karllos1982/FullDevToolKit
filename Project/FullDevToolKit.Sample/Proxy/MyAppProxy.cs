using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;
using MyApp.Models;

namespace MyApp.Proxys
{
    public interface IMyAppProxy
    {
        void Init(HttpClient http, string baseurl, string token);
        
    }

    public class MyAppProxy : APIProxyBase, IMyAppProxy
    {
        public PersonProxy Person = null;

  
        public MyAppProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
			Person = new PersonProxy();

			Person.InitializeAPI(http, baseurl + "/business/person/", token);
			Person.IsAuthenticated = true;
       
        }
      

    }
    

    public class PersonProxy : APIProxyBase
	{

        public PersonProxy()
        {

        }

		public async Task<APIResponse<List<PersonResult>?>> Search(PersonParam data)
		{
			APIResponse<List<PersonResult>?> ret = null;

			ret = await PostAsJSON<List<PersonResult>?>("search", JsonConvert.SerializeObject(data), null);

			return ret;
		}


		public async Task<APIResponse<List<PersonList>?>> List(PersonParam data)
		{
			APIResponse<List<PersonList>?> ret = null;

			ret = await PostAsJSON<List<PersonList>?>("list", JsonConvert.SerializeObject(data), null);

			return ret;
		}

		public async Task<APIResponse<PersonResult?>> Get(string id)
		{
			APIResponse<PersonResult?> ret = null;

			object[] param = new object[1];
			param[0] = new DefaultGetParam(id);

			ret = await GetAsJSON<PersonResult?>("get", param);

			return ret;
		}

		public async Task<APIResponse<PersonEntry?>> Set(PersonEntry data)
		{
			APIResponse<PersonEntry?> ret = null;

			ret = await PostAsJSON<PersonEntry?>("set", JsonConvert.SerializeObject(data), null);

			return ret;
		}


        public async Task<APIResponse<PersonContactEntry?>> ContactEntryValidation(PersonContactEntry data)
        {
			APIResponse<PersonContactEntry?> ret = null;

			ret = await PostAsJSON<PersonContactEntry?>("contactsentryvalidation", 
                JsonConvert.SerializeObject(data), null);

			return ret;
			
        }
    }

  
}
