﻿using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;


namespace MyApp.ViewModel
{
    public class ExceptionLogViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public ExceptionLogViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView();
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public ExceptionLogEntry entry = new ExceptionLogEntry();
        public ExceptionLogResult result = new ExceptionLogResult();
        public ExceptionLogParam param = new ExceptionLogParam();
        public List<ExceptionLogResult> searchresult = new List<ExceptionLogResult>();
        
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

  
        public override async Task Set()
        {


        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<ExceptionLogResult> ret
                = await _Proxys.ExceptionLog.Get(id.ToString());

            SetResult<ExceptionLogResult>(ret, ref result, ref ServiceStatus);

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

            APIResponse<List<ExceptionLogResult>> ret
               = await _Proxys.ExceptionLog.Search(param);

            SetResult<List<ExceptionLogResult>>(ret, ref searchresult, ref ServiceStatus);

        }

    }

}
