using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Collections.Generic;

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
        public List<PermissionResult> searchresult = new List<PermissionResult>();
        public List<RoleList> listRoles = new List<RoleList>();
        public List<UserList> listUsers = new List<UserList>();
        public List<ObjectPermissionList> listObject = new List<ObjectPermissionList>();
        public List<UIBaseItem> listTypeGrant = new List<UIBaseItem>();
        public List<SelectBaseItem> listPermissionValue = new List<SelectBaseItem>();
        
        public string pTypeGrant = "";


        public DefaultLocalization texts = null;

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
        }

        public void LoadTypeGrantList()
        {
            listTypeGrant.Add(new UIBaseItem() { ID = "R", Value = "ByRole" });
            listTypeGrant.Add(new UIBaseItem() { ID = "U", Value = "ByUser" });
        }

        public void LoadListPermissionValue()
        {
            listPermissionValue.Add(new SelectBaseItem() {  Value = 1, Text="Allowed" });
            listPermissionValue.Add(new SelectBaseItem() {  Value = -1, Text = "Denied" });
        }


        public async Task LoadRolesList()
        {
            listRoles = new List<RoleList>();

            ServiceStatus = new ExecutionStatus(true);
            listRoles = await _cacheProxys.ListRoles();

            if (listRoles == null)
            {
                listRoles = new List<RoleList>();
                
            }                   

            listRoles.Insert(0, new RoleList() { RoleID = 0, RoleName = this.texts.Get("SelectItem-Description") });
        }

        public async Task LoadUsersList()
        {
            listUsers = new List<UserList>();
           
            APIResponse<List<UserList>> ret
                 = await _Proxys.User.List(new UserParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    listUsers.AddRange(ret.Data);
                }                
                listUsers.Insert(0, new UserList() { UserID = 0, UserName = this.texts.Get("SelectItem-Description") });
            }          

        }

        public async Task LoadObjectPermissionList()
        {
            listObject = new List<ObjectPermissionList>();

            APIResponse<List<ObjectPermissionList>> ret
                 = await _Proxys.ObjectPermission.List(new ObjectPermissionParam());

            if (ret.IsSuccess)
            {
                if (ret.Data != null)
                {
                    listObject.AddRange(ret.Data);
                }

                listObject.Insert(0, new ObjectPermissionList() { ObjectPermissionID = 0, ObjectName = this.texts.Get("SelectItem-Description") });
            }                       

        }

        public override async Task Set()
        {

            ServiceStatus = new ExecutionStatus(true);

            PermissionEntry entry = new PermissionEntry(result);

            if (entry.UserID == 0) { entry.UserID = null; }
            if (entry.RoleID == 0) { entry.RoleID = null; }

            APIResponse<PermissionEntry> ret
                = await _Proxys.Permission.Set(entry);

            SetResult<PermissionEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<PermissionResult> ret
                = await _Proxys.Permission.Get(id.ToString());

            SetResult<PermissionResult>(ret, ref result, ref ServiceStatus);
                      

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
        
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<PermissionResult>> ret
               = await _Proxys.Permission.Search(param);

            SetResult<List<PermissionResult>>(ret, ref searchresult, ref ServiceStatus);         

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
