using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;
using System.Collections.Generic;
using System.Xml;


namespace MyApp.ViewModel
{
    public class PermissionViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cacheProxys; 

        public PermissionViewModel(SystemProxy service, DataCacheProxy cache ,  
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cacheProxys = cache;  
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public PermissionEntry entry= new PermissionEntry();
        public PermissionResult result = new PermissionResult();
        public PermissionParam param = new PermissionParam() { };
        public PagedList<PermissionResult> searchresult = new PagedList<PermissionResult>();
        public IQueryable<PermissionResult> gridlist = null;
        public List<SelectItemBase> listRoles = new List<SelectItemBase>();
        public List<SelectItemBase> listUsers = new List<SelectItemBase>();
        public List<SelectItemBase> listObject = new List<SelectItemBase>();
        public List<SelectItemBase> listTypeGrant = new List<SelectItemBase>();
        public List<SelectItemBase> listPermissionValue = new List<SelectItemBase>();
        
        public string pTypeGrant = "";
                
        public PermissionSelectStringValues _selectvalues = null; 


        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ObjectPermissionID",""),
                new ExceptionMessage("RoleID",""),
                new ExceptionMessage("UserID","")
            };
           
        }

        public override async Task InitializeModels()
        {

            await ClearSummaryValidation();            

            await LoadRolesList();
            await LoadUsersList();
            await LoadObjectPermissionList();
            LoadTypeGrantList();
            LoadListPermissionValue();

            _selectvalues = new PermissionSelectStringValues();
        }

        public void LoadTypeGrantList()
        {
            listTypeGrant.Add(new SelectItemBase() { Value = "R", Text = "ByRole" });
            listTypeGrant.Add(new SelectItemBase() { Value = "U", Text = "ByUser" });
        }

        public void LoadListPermissionValue()
        {
            listPermissionValue.Add(new SelectItemBase() { Value = "1",  Text="Allowed" });
            listPermissionValue.Add(new SelectItemBase() { Value = "-1", Text = "Denied" });
        }


        public async Task LoadRolesList()
        {
            listRoles = new List<SelectItemBase>();

            ServiceStatus = new ExecutionStatus(true);
            
            List<RoleList> list
                =  await _cacheProxys.ListRoles();

            if (list != null)
            {
                foreach (RoleList role in list)
                {
                    listRoles.Add(new SelectItemBase(role.RoleID.ToString(), role.RoleName));
                }                    
            }                   

            listRoles.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });
        }

        public async Task LoadUsersList()
        {
            listUsers = new List<SelectItemBase>();
           
            APIResponse<List<UserList>> ret
                 = await _Proxys.User.List(new UserParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    foreach (UserList u in ret.Data)
                    {
                        listUsers.Add(new SelectItemBase(u.UserID.ToString(), u.UserName));
                    }                    
                }
                
                listUsers.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });
                
            }          

        }

        public async Task LoadObjectPermissionList()
        {
            listObject = new List<SelectItemBase>();

            APIResponse<List<ObjectPermissionList>> ret
                 = await _Proxys.ObjectPermission.List(new ObjectPermissionParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    foreach (ObjectPermissionList u in ret.Data)
                    {
                        listObject.Add(new SelectItemBase(u.ObjectPermissionID.ToString(), u.ObjectName));
                    }                    
                }

                listObject.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });

             }                       

        }

        public override async Task Set()
        {

            ServiceStatus = new ExecutionStatus(true);

            PermissionEntry entry = new PermissionEntry(result);

            entry.ObjectPermissionID = long.Parse(_selectvalues.ObjectPermissionID);

            long idaux = 0;
            if (long.TryParse(_selectvalues.RoleID, out idaux))
            {
                entry.RoleID = idaux; 
            }

            idaux = 0;
            if (long.TryParse(_selectvalues.UserID, out idaux))
            {
                entry.UserID = idaux;
            }
            
            entry.ReadStatus = int.Parse(_selectvalues.ReadStatus);
            entry.SaveStatus = int.Parse(_selectvalues.SaveStatus);
            entry.DeleteStatus = int.Parse(_selectvalues.DeleteStatus);

            if (entry.UserID == 0) { entry.UserID = null; }
            if (entry.RoleID == 0) { entry.RoleID = null; }

            APIResponse<PermissionEntry> ret
                = await _Proxys.Permission.Set(entry);

            SetResult<PermissionEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {
            ServiceStatus = new ExecutionStatus(true);

            PermissionEntry entry = new PermissionEntry(result);

            APIResponse<PermissionEntry> ret
                = await _Proxys.Permission.Remove(entry);

            SetResult<PermissionEntry>(ret, ref entry, ref ServiceStatus);
        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<PermissionResult> ret
                = await _Proxys.Permission.Get(id.ToString());            

            SetResult<PermissionResult>(ret, ref result, ref ServiceStatus);
                      
            if (result != null)
            {
                _selectvalues.ObjectPermissionID = result.ObjectPermissionID.ToString();
                _selectvalues.RoleID = result.RoleID.ToString();
                _selectvalues.UserID = result.UserID.ToString();
                _selectvalues.SaveStatus = result.SaveStatus.ToString();
                _selectvalues.ReadStatus = result.ReadStatus.ToString();
                _selectvalues.DeleteStatus = result.DeleteStatus.ToString(); 
             }

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
            result = new PermissionResult();
            result.PermissionID = 0;
            result.ReadStatus = 1;
            result.SaveStatus = 1;
            result.DeleteStatus = 1;
            result.TypeGrant = "R";

            _selectvalues = new PermissionSelectStringValues(); 

        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            param.pObjectPermissionID = long.Parse(_selectvalues.pObjectPermissionID);
            param.pRoleID = long.Parse(_selectvalues.pRoleID);
            param.pUserID = long.Parse(_selectvalues.pUserID);


            APIResponse<PagedList<PermissionResult>> ret
               = await _Proxys.Permission.Search(param);

            SetResult<PagedList<PermissionResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.RecordList.AsQueryable();
        }

        public bool GetDisabledStatus(string expected, string typegrant )
        {
            bool ret = false;

            if (typegrant != "")
            {
                if (expected != typegrant)
                {
                    ret = true;
                }
            }

            return ret;
        }

    }
}

public class PermissionSelectStringValues
{
    public string pObjectPermissionID { get; set; } = "0"; 

    public string pRoleID { get; set; } = "0";

    public string pUserID { get; set; } = "0";

    public string ObjectPermissionID { get; set; } = "0";

    public string RoleID { get; set; } = "0";

    public string UserID { get; set; } = "0";

    public string ReadStatus { get; set; } = "-1";

    public string SaveStatus { get; set; } = "-1";

    public string DeleteStatus { get; set; } = "-1";
}

