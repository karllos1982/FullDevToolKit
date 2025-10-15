using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class ObjectPermissionViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public ObjectPermissionViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;
        
        public ObjectPermissionResult result = new ObjectPermissionResult();
        public ObjectPermissionParam param = new ObjectPermissionParam() { pObjectCode="",pObjectName=""};
        public List<ObjectPermissionResult> searchresult = new List<ObjectPermissionResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ObjectName",""),
                new ExceptionMessage("ObjectCode",""),              
            };
         
        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();            

        }


        public override async Task Set()
        {

            ServiceStatus = new ExecutionStatus(true);

            ObjectPermissionEntry entry = new ObjectPermissionEntry(result);

            APIResponse<ObjectPermissionEntry> ret
                = await _Proxys.ObjectPermission.Set(entry);

            SetResult<ObjectPermissionEntry>(ret, ref entry, ref ServiceStatus);


        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<ObjectPermissionResult> ret
                = await _Proxys.ObjectPermission.Get(id.ToString());

            SetResult<ObjectPermissionResult>(ret, ref result, ref ServiceStatus);         

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
            result = new ObjectPermissionResult();
           
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<ObjectPermissionResult>> ret
               = await _Proxys.ObjectPermission.Search(param);

            SetResult<List<ObjectPermissionResult>>(ret, ref searchresult, ref ServiceStatus);

        }

    }
}
