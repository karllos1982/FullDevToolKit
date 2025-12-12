using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;
using Newtonsoft.Json.Linq;
using FullDevToolKit.Core.Common;

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

        public SessionLogProxy SessionLog = null;

        public DataLogProxy DataLog = null;

        public ObjectPermissionProxy ObjectPermission = null;

        public InstanceProxy Instance = null; 

        public PermissionProxy Permission = null;
        
        public LocalizationTextProxy LocalizationText= null;

        public GroupParameterProxy GroupParameter = null;

        public ParameterProxy Parameter = null;

        public ExceptionLogProxy ExceptionLog = null;

        public ConfigsProxy Configs = null;

        public LanguageProxy Language = null; 

        public SystemProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            User = new UserProxy();
            Role = new RoleProxy();
            SessionLog = new SessionLogProxy();
            DataLog = new DataLogProxy();
            ObjectPermission = new ObjectPermissionProxy();
            Permission = new PermissionProxy();
            Instance = new InstanceProxy();
            LocalizationText= new LocalizationTextProxy();
            GroupParameter = new GroupParameterProxy(); 
            Parameter = new ParameterProxy();
            ExceptionLog = new ExceptionLogProxy(); 
            Configs = new ConfigsProxy();  
            Language = new LanguageProxy();

            User.InitializeAPI(http, baseurl + "/system/user/", token);
            User.IsAuthenticated = true;

            Instance.InitializeAPI(http, baseurl + "/system/instance/", token);
            Instance.IsAuthenticated = true;

            Role.InitializeAPI(http, baseurl + "/system/role/", token);
            Role.IsAuthenticated = true;

            SessionLog.InitializeAPI(http, baseurl + "/system/sessionlog/", token);
            SessionLog.IsAuthenticated = true;

            DataLog.InitializeAPI(http, baseurl + "/system/datalog/", token);
            DataLog.IsAuthenticated = true;

            ObjectPermission.InitializeAPI(http, baseurl + "/system/objectpermission/", token);
            ObjectPermission.IsAuthenticated = true;

            Permission.InitializeAPI(http, baseurl + "/system/permission/", token);
            Permission.IsAuthenticated = true;

            LocalizationText.InitializeAPI(http, baseurl + "/system/localizationtext/", token);
            LocalizationText.IsAuthenticated = true;

            GroupParameter.InitializeAPI(http, baseurl + "/system/groupparameter/", token);
            GroupParameter.IsAuthenticated = true;

            Parameter.InitializeAPI(http, baseurl + "/system/parameter/", token);
            Parameter.IsAuthenticated = true;

            ExceptionLog.InitializeAPI(http, baseurl + "/system/exceptionlog/", token);
            ExceptionLog.IsAuthenticated = true;

            Configs.InitializeAPI(http, baseurl + "/system/configs/", token);
            Configs.IsAuthenticated = true;

            Language.InitializeAPI(http, baseurl + "/system/language/", token);
            Language.IsAuthenticated = true;

        }


    }


    public class UserProxy : APIProxyBase
    {
        
        public UserProxy()
        {

        }

        public async Task<APIResponse<PagedList<UserResult>?>> Search(UserParam data)
        {
            APIResponse<PagedList<UserResult>?> ret = null;
            
            ret = await PostAsJSON<PagedList<UserResult>?>("search", JsonConvert.SerializeObject(data),null);
            
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

    public class SessionLogProxy : APIProxyBase
    {

        public SessionLogProxy()
        {

        }

        public async Task<APIResponse<PagedList<SessionLogResult>?>> Search(SessionLogParam data)
        {
            APIResponse<PagedList<SessionLogResult>?> ret = null;

            ret = await PostAsJSON<PagedList<SessionLogResult>?>("search",  JsonConvert.SerializeObject(data), null);

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

        public async Task<APIResponse<PagedList<DataLogResult>?>> Search(DataLogParam data)
        {
            APIResponse<PagedList<DataLogResult>?> ret = null;

            ret = await PostAsJSON<PagedList<DataLogResult>?>("search", JsonConvert.SerializeObject(data), null);


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

       
        public List<DataLogItem> GetDataLogItems(string logcontent)
        {
           
            List<DataLogItem> ret = new List<DataLogItem>();    
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

            ret = list;    

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

        public async Task<APIResponse<PagedList<PermissionResult>?>> Search(PermissionParam param)
        {
            APIResponse<PagedList<PermissionResult>?> ret = null;

            ret = await PostAsJSON<PagedList<PermissionResult>?>("search",
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

        public async Task<APIResponse<PermissionEntry>?> Remove(PermissionEntry data)
        {
            APIResponse<PermissionEntry?> ret = null;

            ret = await PostAsJSON<PermissionEntry?>("remove", JsonConvert.SerializeObject(data), null);

            return ret;
        }
    }

    public class LocalizationTextProxy : APIProxyBase
    {

        public LocalizationTextProxy()
        {

        }

        public async Task<APIResponse<PagedList<LocalizationTextResult>?>> Search(LocalizationTextParam param)
        {
            APIResponse<PagedList<LocalizationTextResult>?> ret = null;

            ret = await PostAsJSON<PagedList<LocalizationTextResult>?>("search",
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

        public async Task<APIResponse<LocalizationTextEntry>?> Remove(LocalizationTextEntry data)
        {
            APIResponse<LocalizationTextEntry?> ret = null;

            ret = await PostAsJSON<LocalizationTextEntry?>("remove", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }

    public class GroupParameterProxy : APIProxyBase
    {

        public GroupParameterProxy()
        {

        }

        public async Task<APIResponse<List<GroupParameterResult>?>> Search(GroupParameterParam data)
        {
            APIResponse<List<GroupParameterResult>?> ret = null;

            ret = await PostAsJSON<List<GroupParameterResult>?>("search", JsonConvert.SerializeObject(data), null);


            return ret;
        }

        public async Task<APIResponse<List<GroupParameterList>?>> List(GroupParameterParam data)
        {
            APIResponse<List<GroupParameterList>?> ret = null;

            ret = await PostAsJSON<List<GroupParameterList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<GroupParameterResult>?> Get(string id)
        {
            APIResponse<GroupParameterResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<GroupParameterResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<GroupParameterEntry?>> Set(GroupParameterEntry data)
        {
            APIResponse<GroupParameterEntry?> ret = null;

            ret = await PostAsJSON<GroupParameterEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }

    }

    public class ParameterProxy : APIProxyBase
    {
        public ParameterProxy()
        {

        }

        public async Task<APIResponse<List<ParameterResult>?>> Search(ParameterParam data)
        {
            APIResponse<List<ParameterResult>?> ret = null;

            ret = await PostAsJSON<List<ParameterResult>?>("search", JsonConvert.SerializeObject(data), null);


            return ret;
        }

        public async Task<APIResponse<List<ParameterList>?>> List(ParameterParam data)
        {
            APIResponse<List<ParameterList>?> ret = null;

            ret = await PostAsJSON<List<ParameterList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<ParameterResult>?> Get(string id)
        {
            APIResponse<ParameterResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<ParameterResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<ParameterEntry?>> Set(ParameterEntry data)
        {
            APIResponse<ParameterEntry?> ret = null;

            ret = await PostAsJSON<ParameterEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }

    }

    public class ExceptionLogProxy : APIProxyBase
    {

        public ExceptionLogProxy()
        {

        }

        public async Task<APIResponse<PagedList<ExceptionLogResult>?>> Search(ExceptionLogParam data)
        {
            APIResponse<PagedList<ExceptionLogResult>?> ret = null;

            ret = await PostAsJSON<PagedList<ExceptionLogResult>?>("search", JsonConvert.SerializeObject(data), null);


            return ret;
        }

        public async Task<APIResponse<List<ExceptionLogList>?>> List(ExceptionLogParam data)
        {
            APIResponse<List<ExceptionLogList>?> ret = null;

            ret = await PostAsJSON<List<ExceptionLogList>?>("list", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<ExceptionLogResult>?> Get(string id)
        {
            APIResponse<ExceptionLogResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<ExceptionLogResult?>("get", param);

            return ret;
        }
        
    }

    public class ConfigsProxy : APIProxyBase
    {

        public ConfigsProxy()
        {

        }

        public async Task<APIResponse<List<ConfigsResult>?>> Search(ConfigsParam param)
        {
            APIResponse<List<ConfigsResult>?> ret = null;

            ret = await PostAsJSON<List<ConfigsResult>?>("search", JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<ConfigsList>?>> List(ConfigsParam param)
        {
            APIResponse<List<ConfigsList>?> ret = null;

            ret = await PostAsJSON<List<ConfigsList>?>("list", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<ConfigsResult>?> Get(string id)
        {
            APIResponse<ConfigsResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<ConfigsResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<ConfigsEntry>?> Set(ConfigsEntry data)
        {
            APIResponse<ConfigsEntry?> ret = null;

            ret = await PostAsJSON<ConfigsEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }
      

    }

    public class LanguageProxy : APIProxyBase
    {

        public LanguageProxy()
        {

        }

        public async Task<APIResponse<List<LanguageResult>?>> Search(LanguageParam param)
        {
            APIResponse<List<LanguageResult>?> ret = null;

            ret = await PostAsJSON<List<LanguageResult>?>("search", JsonConvert.SerializeObject(param), null);


            return ret;
        }

        public async Task<APIResponse<List<LanguageList>?>> List(LanguageParam param)
        {
            APIResponse<List<LanguageList>?> ret = null;

            ret = await PostAsJSON<List<LanguageList>?>("list", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<LanguageResult>?> Get(string id)
        {
            APIResponse<LanguageResult?> ret = null;

            object[] param = new object[1];
            param[0] = new DefaultGetParam(id);

            ret = await GetAsJSON<LanguageResult?>("get", param);

            return ret;
        }

        public async Task<APIResponse<LanguageEntry>?> Set(LanguageEntry data)
        {
            APIResponse<LanguageEntry?> ret = null;

            ret = await PostAsJSON<LanguageEntry?>("set", JsonConvert.SerializeObject(data), null);

            return ret;
        }


    }
}

