using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using FullDevToolKit.Core.Common;


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
        public PagedList<UserResult> searchresult = new PagedList<UserResult>();
        public IQueryable<UserResult> gridlist = null;
        public List<SelectItemBase> listRoles = new List<SelectItemBase>();
        public List<SelectItemBase> listInstances = new List<SelectItemBase>();
        public List<LanguageList> listLangs = new List<LanguageList>();
        

        public UserSelectStringValues _selectvalues = null;
        

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
                new ExceptionMessage("PhoneNumber",""),
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

            _selectvalues = new UserSelectStringValues(); 
        }

        public async Task LoadRolesList()
        {
            listRoles = new List<SelectItemBase>();

            ServiceStatus = new ExecutionStatus(true);

            List<RoleList> list
                = await _cache.ListRoles();

            if (list != null)
            {
                foreach (RoleList role in list)
                {
                    listRoles.Add(new SelectItemBase(role.RoleID.ToString(), role.RoleName));
                }
            }

            listRoles.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });

        }

        public async Task LoadLangsList()
        {
            listLangs = new List<LanguageList>();

            ServiceStatus = new ExecutionStatus(true);
            listLangs = await _cache.ListLanguages();

            if (listLangs == null)
            {
                listLangs = new List<LanguageList>();

            }

            listLangs.Insert(0, new () { LanguageID = 0, LanguageName = this.texts.Get("SelectItem-Description") });

        }

        public async Task LoadInstancesList()
        {
            listInstances = new List<SelectItemBase>();

            APIResponse<List<InstanceList>> ret
                 = await _Proxys.Instance.List(new InstanceParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    foreach (InstanceList u in ret.Data)
                    {
                        listInstances.Add(new SelectItemBase(u.InstanceID.ToString(), u.InstanceName));
                    }
                }

                listInstances.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });

            }
 
        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);
            
            UserEntry entry = new UserEntry(result);

            entry.LanguageID = long.Parse(_selectvalues.LanguageID); 
            MergeRole(ref entry, result.Roles, long.Parse(_selectvalues.SelectedRole));
            MergeInstance(ref entry, result.Instances, long.Parse(_selectvalues.SelectedInstance));

            APIResponse<UserEntry> ret
                = await _Proxys.User.Set(entry);

            SetResult<UserEntry>(ret, ref entry, ref ServiceStatus);
          
        }

        public override async Task Remove()
        {

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
                _selectvalues.SelectedRole = result.Roles[0].RoleID.ToString();
                _selectvalues.SelectedInstance = result.Instances[0].InstanceID.ToString();
                _selectvalues.LanguageID = result.LanguageID.ToString(); 
                
                this.result.PhoneNumber = FormatCellPhoneNumber(result.PhoneNumber);
            }
           
        }

        public async Task CreateNew()
        {
            ServiceStatus = new ExecutionStatus(true);

            newModel.RoleID = long.Parse(_selectvalues.RoleID);
            newModel.InstanceID = long.Parse(_selectvalues.InstanceID);
            newModel.LanguageID = long.Parse(_selectvalues.LanguageID);

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
            
            _selectvalues.RoleID = "0";
            _selectvalues.InstanceID = "0"; 
            _selectvalues.LanguageID = "0"; 

            newModel = new NewUser();
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            param.pRoleID = long.Parse(_selectvalues.pRoleID);
            param.pInstanceID = long.Parse(_selectvalues.pInstanceID);
            
            APIResponse<PagedList<UserResult>> ret
               = await _Proxys.User.Search(param);

            SetResult<PagedList<UserResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.RecordList.AsQueryable();
        }   

    }
}

public class UserSelectStringValues
{
    
    public string pRoleID { get; set; } = "0";    

    public string pInstanceID{ get; set; } = "0";

    public string SelectedRole { get; set; } = "0";

    public string SelectedInstance { get; set; } = "0";

    public string RoleID { get; set; } = "0";

    public string InstanceID { get; set; } = "0";

    public string LanguageID { get; set; } = "0"; 

}