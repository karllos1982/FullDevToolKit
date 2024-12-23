﻿using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;

namespace MyApp.Proxys
{
    public interface IMyAppProxy
    {
        void Init(HttpClient http, string baseurl, string token);
        
    }

    public class MyAppProxy : APIProxyBase, IMyAppProxy
    {
        public ClientGateway Client = null;

  
        public MyAppProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            Client = new ClientGateway();
          
            Client.InitializeAPI(http, baseurl + "/template/client/", token);
            Client.IsAuthenticated = true;
       
        }
      

    }
    

    public class ClientGateway : APIGatewayManagerAsync
    {

        public ClientGateway()
        {

        }

        public async Task<List<ClientResult>> Search(ClientParam data)
        {
            List<ClientResult> ret = null;

            ret = await PostAsJSON<List<ClientResult>>("search",
                JsonConvert.SerializeObject(data), null);


            return ret;
        }

        public async Task<List<ClientList>> List(ClientParam data)
        {
            List<ClientList> ret = null;

            ret = await PostAsJSON<List<ClientList>>("list",
                JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<ClientResult> Get(string id)
        {
            ClientResult ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<ClientResult>("get", param);

            return ret;
        }

        public async Task<ClientEntry> Set(ClientEntry data)
        {
            ClientEntry ret = null;

            ret = await PostAsJSON<ClientEntry>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


        public async Task<ClientContactsEntry> ContactEntryValidation(ClientContactsEntry data)
        {
            ClientContactsEntry ret = null;

            ret = await PostAsJSON<ClientContactsEntry>("contactsentryvalidation",
                JsonConvert.SerializeObject(data), null);

            return ret;
        }
    }

  
}
