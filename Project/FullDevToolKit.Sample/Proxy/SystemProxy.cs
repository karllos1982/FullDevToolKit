using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;
using Newtonsoft.Json.Linq;

namespace MyApp.Proxys
{
    public interface ISystemProxyManager
    {
        void Init(HttpClient http, string baseurl, string token);

  
    }

    public class SystemProxy: APIProxyBase, ISystemProxyManager
    {
        public UserProxy User = null;

        public RoleProxy Role = null;

        public SessionProxy Session = null;

        public DataLogProxy DataLog = null;

        public ObjectPermissionProxy ObjectPermission = null;

        public InstanceProxy Instance = null; 

        public PermissionProxy Permission = null;
        
        public LocalizationTextProxy LocalizationText= null;

        
        public SystemProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            User = new UserProxy();
            Role = new RoleProxy();
            Session = new SessionProxy();
            DataLog = new DataLogProxy();
            ObjectPermission = new ObjectPermissionProxy();
            Permission = new PermissionProxy();
            Instance = new InstanceProxy();
            LocalizationText= new LocalizationTextProxy();

            User.InitializeAPI(http, baseurl + "/membership/user/", token);
            User.IsAuthenticated = true;

            Instance.InitializeAPI(http, baseurl + "/membership/instance/", token);
            Instance.IsAuthenticated = true;

            Role.InitializeAPI(http, baseurl + "/membership/role/", token);
            Role.IsAuthenticated = true;

            Session.InitializeAPI(http, baseurl + "/membership/session/", token);
            Session.IsAuthenticated = true;

            DataLog.InitializeAPI(http, baseurl + "/membership/datalog/", token);
            DataLog.IsAuthenticated = true;

            ObjectPermission.InitializeAPI(http, baseurl + "/membership/objectpermission/", token);
            ObjectPermission.IsAuthenticated = true;

            Permission.InitializeAPI(http, baseurl + "/membership/permission/", token);
            Permission.IsAuthenticated = true;

            LocalizationText.InitializeAPI(http, baseurl + "/membership/localizationtext/", token);
            LocalizationText.IsAuthenticated = true;

        }
     

    }


    public class UserProxy : APIProxyBase
    {
        
        public UserProxy()
        {

        }

        public async Task<APIResponse<List<UserResult>?>> Search(UserParam data)
        {
            APIResponse<List<UserResult>?> ret = null;
            
            ret = await PostAsJSON<List<UserResult>?>("search", JsonConvert.SerializeObject(data),null);
            
            return ret;
        }

        public async Task<APIResponse<List<UserList>?>> List(UserParam data)
        {
            APIResponse<List<UserList>?> ret = null;
            
            ret = await PostAsJSON<List<UserList>?>("list", JsonConvert.SerializeObject(data),null);
           
            return ret;
        }

        public async Task<APIResponse<UserResult?>> Get(string id)
        {
            APIResponse<UserResult?> ret = null;

            object[] param = new object[1];       
            param[0] = new DefaultGetParam(id) ;
            
            ret = await GetAsJSON<UserResult?>("get", param);
           
            return ret;
        }

        public async Task<APIResponse<UserEntry?>> Set(UserEntry data)
        {
            APIResponse<UserEntry?> ret = null;

            ret = await PostAsJSON<UserEntry?>("set", JsonConvert.SerializeObject(data),null);            

            return ret;
        }

        public async Task<APIResponse<UserEntry?>> CreateNewUser(NewUser data)
        {
            APIResponse<UserEntry?> ret = null;
            
            ret = await this.PostAsJSON<UserEntry?>("createnewuser",JsonConvert.SerializeObject(data), null);
                       
            return ret;
        }

        public async Task<APIResponse<UserRolesEntry?>> AddToRole(string userid, string roleid)
        {
            APIResponse<UserRolesEntry?> ret = null;
            UserRolesEntry data = new UserRolesEntry()
            {
                UserID = Int64.Parse(userid),
                RoleID = Int64.Parse(roleid)
            }
            ;
            ret = await this.PostAsJSON<UserRolesEntry?>("addtorole",JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<UserRolesEntry?>> RemoveFromRole(string userid, string roleid)
        {
            APIResponse<UserRolesEntry?> ret = null;
            UserRolesEntry data = new UserRolesEntry()
            {
                UserID = Int64.Parse(userid),
                RoleID = Int64.Parse(roleid)
            }
            ;
            ret = await this.PostAsJSON<UserRolesEntry?>("removefromrole", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<bool>> AlterInstance(UserInstancesResult data)
        {
            APIResponse<bool> ret = null;
            
            ret = await this.PostAsJSON<bool>("alterinstance", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<bool>> AlterRole(UserRolesResult data)
        {
            APIResponse<bool> ret = null;

            ret = await this.PostAsJSON<bool>("alterrole", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<bool>> ChangeState(UserChangeState data)
        {
            APIResponse<bool> ret = null;

            ret = await this.PostAsJSON<bool>("changestate", JsonConvert.SerializeObject(data), null);

            return ret;
        }
       
    }

    public class InstanceProxy : APIProxyBase
    {

        public InstanceProxy()
        {

        }

        public async Task<APIResponse<List<InstanceResult>?>> Search(InstanceParam param)
        {
            APIResponse<List<InstanceResult>?> ret = null;

            ret = await PostAsJSON<List<InstanceResult>?>("search", JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<InstanceList>?>> List(InstanceParam param)
        {
            APIResponse<List<InstanceList>?> ret = null;

            ret = await PostAsJSON<List<InstanceList>?>("list", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<InstanceResult>?> Get(string id)
        {
            APIResponse<InstanceResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<InstanceResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<InstanceEntry>?> Set(InstanceEntry data)
        {
            APIResponse<InstanceEntry?> ret = null;

            ret = await PostAsJSON<InstanceEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }


    public class RoleProxy : APIProxyBase
    {
     
        public RoleProxy()
        {

        }

        public async Task<APIResponse<List<RoleResult>?>> Search(RoleParam param)
        {
            APIResponse<List<RoleResult>?> ret = null;

            ret = await PostAsJSON<List<RoleResult>?>("search", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<List<RoleList>?>> List(RoleParam param)
        {
            APIResponse<List<RoleList>?> ret = null;

            ret = await PostAsJSON<List<RoleList>?>("list", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<RoleResult?>> Get(string id)
        {
            APIResponse<RoleResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<RoleResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<RoleEntry?>> Set(RoleEntry data)
        {
            APIResponse<RoleEntry?> ret = null;

            ret = await PostAsJSON<RoleEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }

    public class SessionProxy : APIProxyBase
    {

        public SessionProxy()
        {

        }

        public async Task<APIResponse<List<SessionLogResult>?>> Search(SessionLogParam data)
        {
            APIResponse<List<SessionLogResult>?> ret = null;

            ret = await PostAsJSON<List<SessionLogResult>?>("search",  JsonConvert.SerializeObject(data), null);

            return ret;
        }


        public async Task<APIResponse<List<SessionLogList>?>> List(SessionLogParam data)
        {
            APIResponse<List<SessionLogList>?> ret = null;

            ret = await PostAsJSON<List<SessionLogList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }


        public async Task<APIResponse<SessionLogResult>?> Get(string id)
        {
            APIResponse<SessionLogResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<SessionLogResult?>("get", param);

            return ret;
        }
            

    }

    public class DataLogProxy : APIProxyBase
    {

        public DataLogProxy()
        {

        }

        public async Task<APIResponse<List<DataLogResult>?>> Search(DataLogParam data)
        {
            APIResponse<List<DataLogResult>?> ret = null;

            ret = await PostAsJSON<List<DataLogResult>?>("search", JsonConvert.SerializeObject(data), null);


            return ret;
        }

        public async Task<APIResponse<List<DataLogList>?>> List(DataLogParam data)
        {
            APIResponse<List<DataLogList>?> ret = null;

            ret = await PostAsJSON<List<DataLogList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<DataLogResult>?> Get(string id)
        {
            APIResponse<DataLogResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<DataLogResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<List<TabelasValueModel>?>> GetTableList()
        {
            APIResponse<List<TabelasValueModel>?> ret = null;
            
            ret = await GetAsJSON<List<TabelasValueModel>?>("get", null);

            return ret;
        }

        public async Task<APIResponse<List<DataLogTimelineModel>?>> GetTimeLine(string recordID)
        {
            APIResponse<List<DataLogTimelineModel>?> ret = null;
            
            object[] param = new object[1];
            param[0] = new DefaultGetParam(recordID);

            ret = await GetAsJSON<List<DataLogTimelineModel>?>("gettimeline", param);          

            return ret;
        }

       
        public APIResponse<List<DataLogItem>?> GetDataLogItems(string logcontent)
        {
           
            APIResponse<List<DataLogItem>?> ret = new APIResponse<List<DataLogItem>?>();    
            List<DataLogItem> list = new List<DataLogItem>();
            DataLogItem obj;

            dynamic aux = JsonConvert.DeserializeObject<object>(logcontent);

            JProperty jprop;
            JObject jobj = (JObject)aux;
            JToken jtk;

            foreach (JToken p in jobj.Children())
            {
                jprop = (JProperty)p;
                jtk = (JToken)jprop.Value;

                obj = new DataLogItem();
                obj.ItemName = jprop.Name;
                obj.ItemValue = jtk.ToString();
                obj.IsDifferent = false;
                list.Add(obj);
            }            

            ret.Data = list;    

            return ret;
        }

        public void GetDataLogDiff(ref List<DataLogItem> old, ref List<DataLogItem> current)
        {
            DataLogItem objlog;

            foreach (DataLogItem l in current)
            {
                objlog = old.Where(x => x.ItemName == l.ItemName).FirstOrDefault();

                if (objlog.ItemValue != l.ItemValue)
                {
                    l.IsDifferent = true;
                    objlog.IsDifferent = true;
                }
            }
        }

    }

    public class ObjectPermissionProxy : APIProxyBase
    {

        public ObjectPermissionProxy()
        {

        }

        public async Task<APIResponse<List<ObjectPermissionResult>?>> Search(ObjectPermissionParam param)
        {
            APIResponse<List<ObjectPermissionResult>?> ret = null;

            ret = await PostAsJSON<List<ObjectPermissionResult>?>("search",
                JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<ObjectPermissionList>?>> List(ObjectPermissionParam param)
        {
            APIResponse<List<ObjectPermissionList>?> ret = null;

            ret = await PostAsJSON<List<ObjectPermissionList>?>("list",
                JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<ObjectPermissionResult?>> Get(string id)
        {
            APIResponse<ObjectPermissionResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<ObjectPermissionResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<ObjectPermissionEntry>?> Set(ObjectPermissionEntry data)
        {
            APIResponse<ObjectPermissionEntry?> ret = null;

            ret = await PostAsJSON<ObjectPermissionEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }

    public class PermissionProxy : APIProxyBase
    {

        public PermissionProxy()
        {

        }

        public async Task<APIResponse<List<PermissionResult>?>> Search(PermissionParam param)
        {
            APIResponse<List<PermissionResult>?> ret = null;

            ret = await PostAsJSON<List<PermissionResult>?>("search",
                JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<PermissionList>?>> List(PermissionParam param)
        {
            APIResponse<List<PermissionList>?> ret = null;

            ret = await PostAsJSON<List<PermissionList>?>("list",
                JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<PermissionResult>?> Get(string id)
        {
            APIResponse<PermissionResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<PermissionResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<PermissionEntry>?> Set(PermissionEntry data)
        {
            APIResponse<PermissionEntry?> ret = null;

            ret = await PostAsJSON<PermissionEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<PermissionEntry>?> Delete(PermissionEntry data)
        {
            APIResponse<PermissionEntry?> ret = null;

            ret = await PostAsJSON<PermissionEntry?>("delete", JsonConvert.SerializeObject(data), null);

            return ret;
        }
    }


    public class LocalizationTextProxy : APIProxyBase
    {

        public LocalizationTextProxy()
        {

        }

        public async Task<APIResponse<List<LocalizationTextResult>?>> Search(LocalizationTextParam param)
        {
            APIResponse<List<LocalizationTextResult>?> ret = null;

            ret = await PostAsJSON<List<LocalizationTextResult>?>("search",
                JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<LocalizationTextList>?>> List(LocalizationTextParam param)
        {
            APIResponse<List<LocalizationTextList>?> ret = null;

            ret = await PostAsJSON<List<LocalizationTextList>?>("list",
                JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<LocalizationTextResult?>> Get(string id)
        {
            APIResponse<LocalizationTextResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<LocalizationTextResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<LocalizationTextEntry?>> Set(LocalizationTextEntry data)
        {
            APIResponse<LocalizationTextEntry?> ret = null;

            ret = await PostAsJSON<LocalizationTextEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }
}

