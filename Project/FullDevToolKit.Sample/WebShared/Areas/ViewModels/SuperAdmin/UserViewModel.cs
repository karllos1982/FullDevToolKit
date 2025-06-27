using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;


namespace MyApp.ViewModel
{
    public class UserViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache; 

        public UserViewModel(SystemProxy service, DataCacheProxy cache,
                UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache= cache;
            this.InitializeView(_user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;


        public UserResult result = new UserResult();
        public NewUser newModel = new NewUser();
        public UserParam param = new UserParam();
        public List<UserResult> searchresult = new List<UserResult>();
        public List<RoleList> listRoles = new List<RoleList>();
        public List<InstanceList> listInstances = new List<InstanceList>();
        public List<LocalizationTextList> listLangs = new List<LocalizationTextList>();
        
        public Int64 selectedRole = 0;
        public Int64 selectedInstance = 0;

        public DefaultLocalization texts = null;

        public bool isUserActive { get; set; }
        public bool isUserLocked { get; set; }

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("InstanceID",""),
                new ExceptionMessage("RoleID",""),
                new ExceptionMessage("Email",""),
                new ExceptionMessage("UserName",""),
                new ExceptionMessage("Password",""),
                new ExceptionMessage("DefaultLanguage","")
            };

        }

        public override async Task InitializeModels()
        {

            await ClearSummaryValidation();            

            await LoadRolesList();
            await LoadInstancesList();
            await LoadLangsList();
          
        }

        public async Task LoadRolesList()
        {
            listRoles = new List<RoleList>();

            ServiceStatus = new ExecutionStatus(true);
            listRoles = await _cache.ListRoles();

            if (listRoles == null)
            {
                listRoles = new List<RoleList>();

            }

            listRoles.Insert(0, new RoleList() { RoleID = 0, RoleName = this.texts.Get("SelectItem-Description") });
        }

        public async Task LoadLangsList()
        {
            listLangs = new List<LocalizationTextList>();

            ServiceStatus = new ExecutionStatus(true);
            listLangs = await _cache.ListLanguages();

            if (listLangs == null)
            {
                listLangs = new List<LocalizationTextList>();

            }

            listLangs.Insert(0, new LocalizationTextList() { LocalizationTextID = 0, Language = this.texts.Get("SelectItem-Description") });

        }

        public async Task LoadInstancesList()
        {
            listInstances = new List<InstanceList>();

            APIResponse<List<InstanceList>> ret
                 = await _Proxys.Instance.List(new InstanceParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    listInstances.AddRange(ret.Data);
                }               
            }

            listInstances.Insert(0, new InstanceList() { InstanceID = 0, InstanceName = this.texts.Get("SelectItem-Description") });
        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);
            
            UserEntry entry = new UserEntry(result);

            MergeRole(ref entry, result.Roles, selectedRole);
            MergeInstance(ref entry, result.Instances, selectedInstance);

            APIResponse<UserEntry> ret
                = await _Proxys.User.Set(entry);

            SetResult<UserEntry>(ret, ref entry, ref ServiceStatus);
          
        }
        
        private void MergeRole(ref UserEntry obj,
                List<UserRolesResult> roles, Int64 roleid)
        {
            UserRolesEntry  newentry;

            obj.Roles = new List<UserRolesEntry>();

            newentry = new UserRolesEntry(roles[0]);
            newentry.RoleID = roleid;
            newentry.RecordState = RECORDSTATEENUM.EDITED;
            obj.Roles.Add(newentry);
                     
        }

        private void MergeInstance(ref UserEntry obj,
                List<UserInstancesResult> instances, Int64 instanceid)
        {
            UserInstancesEntry newentry;

            obj.Instances = new List<UserInstancesEntry>();

            newentry = new UserInstancesEntry(instances[0]);
            newentry.InstanceID = instanceid;
            newentry.RecordState = RECORDSTATEENUM.EDITED;
            obj.Instances.Add(newentry);
          
        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<UserResult> ret
                = await _Proxys.User.Get(id.ToString());

            SetResult<UserResult>(ret, ref result, ref ServiceStatus);

            if (ret.IsSuccess && ret.Data != null)
            {
                this.isUserActive = Convert.ToBoolean(result.IsActive);
                this.isUserLocked = Convert.ToBoolean(result.IsLocked);
                selectedRole = result.Roles[0].RoleID;
                selectedInstance = result.Instances[0].InstanceID;
            }
           
        }

        public async Task CreateNew()
        {
            ServiceStatus = new ExecutionStatus(true);
            
             APIResponse<UserEntry> ret
                = await _Proxys.User.CreateNewUser(newModel);

            UserEntry resultUser = new UserEntry(); 
            SetResult<UserEntry>(ret, ref resultUser, ref ServiceStatus);
        }

        public async Task ChangeState()
        {
            UserChangeState state = new UserChangeState();
            ServiceStatus = new ExecutionStatus(true);

            state.UserID = result.UserID;
            state.ActiveValue = false;
            state.LockedValue = false;

            state.ActiveValue = this.isUserActive; 
            state.LockedValue = this.isUserLocked;

            APIResponse<bool> ret
                = await _Proxys.User.ChangeState(state);
            bool changeResult = false; 
            SetResult<bool>(ret, ref changeResult, ref ServiceStatus);
            
        }

        public override void BackToSearch()
        {
            this.BaseBack();

        }

        public override void InitEdit()
        {
            this.BaseInitEdit();

        }

        public override void InitNew()
        {
            this.BaseInitNew();
            newModel = new NewUser();
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<UserResult>> ret
               = await _Proxys.User.Search(param);

            SetResult<List<UserResult>>(ret, ref searchresult, ref ServiceStatus);

        }   

    }
}
