using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class SessionLogViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public SessionLogViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView();
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;


        public SessionLogEntry entry = new SessionLogEntry();
        public SessionLogResult result = new SessionLogResult();
        public SessionLogParam param = new SessionLogParam();
        public PagedList<SessionLogResult> searchresult = new PagedList<SessionLogResult>();
        public IQueryable<SessionLogResult> gridlist = null;

        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; }

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {

            };
           
        }

        public override async Task InitializeModels()
        {
            param.pDate_Start = DateTime.Now.AddDays(-7);
            param.pData_End = DateTime.Now;

            await ClearSummaryValidation();          

        }

        public override async Task Set()
        {


        }

        public override async Task Remove()
        {

        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<SessionLogResult> ret
                = await _Proxys.SessionLog.Get(id.ToString());

            SetResult<SessionLogResult>(ret, ref result, ref ServiceStatus);
         
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
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            if (dataInicio != null && dataFim != null)
            {
                param.pDate_Start = dataInicio.Value;
                param.pData_End = dataFim.Value;
                param.SearchByDate = true;
            }
            else
            {
                dataInicio = null;
                dataFim = null;
            }

            param.RecordsPerPage = 20;
            
            APIResponse<PagedList<SessionLogResult>> ret
               = await _Proxys.SessionLog.Search(param);

            SetResult<PagedList<SessionLogResult>>(ret, ref searchresult, ref ServiceStatus);
                  
            gridlist = searchresult.RecordList.AsQueryable();

        }

    }
}
