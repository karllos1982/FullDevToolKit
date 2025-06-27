using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class DataLogViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public DataLogViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView();
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;


        public DataLogEntry entry = new DataLogEntry();
        public DataLogResult result = new DataLogResult();
        public DataLogParam param = new DataLogParam();
        public List<DataLogResult> searchresult = new List<DataLogResult>();
        public List<TipoOperacaoValueModel> listTipoOperacao = new List<TipoOperacaoValueModel>();
        public List<TabelasValueModel> listTabelas = new List<TabelasValueModel>();

        public List<DataLogItem> logold_content;
        public List<DataLogItem> logcurrent_content;
        public List<DataLogTimelineModel> timeline = null;
        public bool ShowTimeline = false;

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {

            };
          
        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();         
            
        }

        public async Task LoadTipoOperacaoList(DataCacheProxy cache)
        {

            ServiceStatus = new ExecutionStatus(true);
            listTipoOperacao = await cache.ListTipoOperacao();

            if (listTipoOperacao != null)
            {
                listTipoOperacao.Insert(0, new TipoOperacaoValueModel() { Value = "0", Text = this.texts.Get("AllItem-Description") });
            }
            else
            {
                listTipoOperacao = new List<TipoOperacaoValueModel>();
            }

        }

        public async Task LoadTabelaList(DataCacheProxy cache)
        {

            ServiceStatus = new ExecutionStatus(true);
            listTabelas = await cache.ListTabelas();

            if (listTabelas != null)
            {
                listTabelas.Insert(0, new TabelasValueModel() { Value = "0", Text = this.texts.Get("AllItem-Description") });
            }
            else
            {
                listTabelas = new List<TabelasValueModel>();
            }

        }

        public override async Task Set()
        {


        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<DataLogResult> ret
                = await _Proxys.DataLog.Get(id.ToString());

            SetResult<DataLogResult>(ret, ref result, ref ServiceStatus); 
          
        }

        public async Task GetTimeline()
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<DataLogTimelineModel>?> ret = 
                await _Proxys.DataLog.GetTimeLine(result.ID.ToString());

            SetResult<List<DataLogTimelineModel>>(ret, ref timeline, ref ServiceStatus);

        }

        public override void BackToSearch()
        {
            this.BaseBack();
            this.ShowTimeline = false;
        }

        public override void InitEdit()
        {
            this.BaseInitEdit();

            if (result != null)
            {
                GetDataLogContent(result);

            }

        }

        public void GetDataLogContent(DataLogResult log)
        {
            logold_content = new List<DataLogItem>();
            logcurrent_content = new List<DataLogItem>();

            if (log.LogOldData != null)
            {
                if (log.LogOldData != "")
                {
                    logold_content = _Proxys.DataLog
                        .GetDataLogItems(log.LogOldData);
                }
            }

            if (log.LogCurrentData != null)
            {
                if (log.LogCurrentData != "")
                {
                    logcurrent_content = _Proxys.DataLog
                        .GetDataLogItems(log.LogCurrentData);
                }
            }

            if (log.Operation == "U")
            {
                _Proxys.DataLog
                    .GetDataLogDiff(ref logold_content, ref logcurrent_content);
            }

        }

        public override void InitNew()
        {
            this.BaseInitNew();
        }

        public override async Task Search()
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<DataLogResult>> ret
               = await _Proxys.DataLog.Search(param);

            SetResult<List<DataLogResult>>(ret, ref searchresult, ref ServiceStatus);
          
        }

    }

}
