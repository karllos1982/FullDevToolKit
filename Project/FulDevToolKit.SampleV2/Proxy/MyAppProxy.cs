using FullDevToolKit.Common;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;
using MyApp.Models;
using FullDevToolKit.Core.Common;

namespace MyApp.Proxys
{
    public interface IMyAppProxy
    {
        void Init(HttpClient http, string baseurl, string token);
    }

    public class MyAppProxy : APIProxyBase, IMyAppProxy
    {
        public PersonProxy Person = null;
        public SimpleEntityProxy SimpleEntity = null;

        public MyAppProxy()
        {
        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            Person = new PersonProxy();
            Person.InitializeAPI(http, baseurl + "/myapp/person/", token);
            Person.IsAuthenticated = true;

            SimpleEntity = new SimpleEntityProxy();
            SimpleEntity.InitializeAPI(http, baseurl + "/myapp/simpleentity/", token);
            SimpleEntity.IsAuthenticated = true;
        }
    }

    public class SimpleEntityProxy : APIProxyBase
    {
        public SimpleEntityProxy()
        {
        }

        public async Task<APIResponse<PagedList<SimpleEntityResult>?>> Search(SimpleEntityParam data)
        {
            APIResponse<PagedList<SimpleEntityResult>?> ret = null;

            ret = await PostAsJSON<PagedList<SimpleEntityResult>?>("search", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<List<SimpleEntityList>?>> List(SimpleEntityParam data)
        {
            APIResponse<List<SimpleEntityList>?> ret = null;

            ret = await PostAsJSON<List<SimpleEntityList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<SimpleEntityResult?>> Get(string id)
        {
            APIResponse<SimpleEntityResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<SimpleEntityResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<SimpleEntityEntry?>> Set(SimpleEntityEntry data)
        {
            APIResponse<SimpleEntityEntry?> ret = null;

            ret = await PostAsJSON<SimpleEntityEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<SimpleEntityEntry?>> Remove(SimpleEntityEntry data)
        {
            APIResponse<SimpleEntityEntry?> ret = null;

            ret = await PostAsJSON<SimpleEntityEntry?>("remove", JsonConvert.SerializeObject(data), null);

            return ret;
        }
    }
}