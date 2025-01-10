using FullDevToolKit.Common;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;


namespace FullDevToolKit.ApplicationHelpers
{
    
    public class APIProxyBase
    {
       
        public APIProxyBase()
        {

        }

        public void InitializeAPI(HttpClient http, string baseurl, string token)
        {
            this.Requestor = new HttpConnection(http, baseurl); 
            this.BaseURL = baseurl;
            this.Token = new AuthToken() { TokenValue = token };            
        }

        public bool IsAuthenticated { get; set; }

        public HttpConnection Requestor { get; set; }
               
        public string BaseURL { get; set; }

        public AuthToken Token { get; set; }

        private void SetRequest(string url)
        {
            Requestor.InitRequest(); 
            Requestor.ClearHeaders();            
            Requestor.AddHeader("Authorization", "Bearer " + Token.TokenValue); 
        }

    
        public async Task<APIResponse<T?>> GetAsJSON<T>(string endpoint,object[] param)            
        {
            APIResponse<T?>? ret = null;

            SetRequest(endpoint);

            HttpResponseMessage exec = await Requestor.Get(endpoint, param);          
           
            ret = await exec.Content.ReadFromJsonAsync<APIResponse<T?>>();

            return ret ?? GetDefaultResult<T?>();
        }

        public async Task<APIResponse<T?>> PostAsJSON<T>(string endpoint, string data, object[] param)
        {
            APIResponse<T?>? ret = null;

            SetRequest(endpoint);

            HttpResponseMessage exec = await Requestor.PostAsJson(endpoint, param,data);

            ret = await exec.Content.ReadFromJsonAsync<APIResponse<T?>>();

            return ret ?? GetDefaultResult<T?>();
        }
        
        public async Task<APIResponse<T?>> PostAsStream<T>(string endpoint, byte[] data, object[] param)
        {
            APIResponse<T?>? ret = null;

            SetRequest(endpoint);

            HttpResponseMessage exec = await Requestor.PostAsStream(endpoint, param, data);

            ret = await exec.Content.ReadFromJsonAsync<APIResponse<T?>>();

            return ret ?? GetDefaultResult<T?>();
        }


        public APIResponse<T> GetDefaultResult<T>()
        {
            ExecutionExceptions exps = new ExecutionExceptions();
            exps.AddException("UnknowError", LocalizationText.Get("API-Unexpected-Exception", "eng").Text);
            return new APIResponse<T>( 400, exps);

        }
      
    }


}