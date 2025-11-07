using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using FullDevToolKit.Core.Common;

namespace MyApp.ViewModel
{
    public class ParameterViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public ParameterViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public ParameterResult result = new ParameterResult();
        public ParameterParam param = new ParameterParam() { };
        public List<ParameterResult> searchresult = new List<ParameterResult>();
        public List<SelectItemBase> listgroupparameter = new List<SelectItemBase>();
        public IQueryable<ParameterResult> gridlist = null;

        public ParameterSelectStringValues _selectvalues = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ParameterName",""),
                new ExceptionMessage("GroupParameterID",""),                
                new ExceptionMessage("IsActive", ""),
            };

        }

        public async Task LoadGroupParameterList()
        {
            listgroupparameter = new List<SelectItemBase>();

            ServiceStatus = new ExecutionStatus(true);

            List<GroupParameterList> list
                = await _cache.ListGroupParameter();

            if (list != null)
            {
                foreach (GroupParameterList g in list)
                {
                    listgroupparameter.Add(new SelectItemBase(g.GroupParameterID.ToString(), g.GroupParameterName));
                }
            }

            listgroupparameter.Insert(0, new SelectItemBase() { Value = "0", Text = this.texts.Get("SelectItem-Description") });
        
        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();    
            await LoadGroupParameterList();

            _selectvalues = new ParameterSelectStringValues();
        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            ParameterEntry entry = new ParameterEntry(result);

            entry.GroupParameterID = long.Parse(_selectvalues.GroupParameterID);

            APIResponse<ParameterEntry> ret
                = await _Proxys.Parameter.Set(entry);

            SetResult<ParameterEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {
            
        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<ParameterResult> ret
                = await _Proxys.Parameter.Get(id.ToString());

            SetResult<ParameterResult>(ret, ref result, ref ServiceStatus);

            if (result != null)
            {
                _selectvalues.GroupParameterID = result.GroupParameterID.ToString(); 
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
            result = new ParameterResult();
            result.IsActive = true;
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            param.pGroupParameterID = long.Parse(_selectvalues.pGroupParameterID);

            APIResponse<List<ParameterResult>> ret
               = await _Proxys.Parameter.Search(param);

            SetResult<List<ParameterResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.AsQueryable();

        }

    }

    public class ParameterSelectStringValues
    {
        public string pGroupParameterID { get; set; } = "0";

        public string GroupParameterID { get; set; } = "0";

    }


 }