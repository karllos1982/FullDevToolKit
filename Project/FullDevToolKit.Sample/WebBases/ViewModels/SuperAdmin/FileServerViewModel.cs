using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common; 
using MyApp.Proxys;
using FullDevToolKit.Core.Common;
using Newtonsoft.Json;


namespace MyApp.ViewModel
{
    public class FileServerViewModel : BaseViewModel
    {

        private FileServerProxy _Proxys;
        
        public FileServerViewModel(FileServerProxy service,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;            
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;
      
        public List<DirectoryResult> dirList = new List<DirectoryResult>();
        public IQueryable<FileResult> fileList = null;
        
        public string dirSelected = ""; 

        public async Task<FileOperationResult> UploadFile(FileEntry data)
        {
            ServiceStatus = new ExecutionStatus(true);
            FileOperationResult ret = new FileOperationResult(); 

            APIResponse<FileOperationResult> aux
               = await _Proxys.UploadFile(data);

            SetResult<FileOperationResult>(aux, ref ret, ref ServiceStatus);

            return ret;
        }


        public async Task<FileOperationResult> DeleteFile(FileEntry data)
        {
            ServiceStatus = new ExecutionStatus(true);
            FileOperationResult ret = new FileOperationResult();

            APIResponse<FileOperationResult> aux
               = await _Proxys.DeleteFile(data);

            SetResult<FileOperationResult>(aux, ref ret, ref ServiceStatus);

            return ret;
        }

        public async Task<FileOperationResult> MoveFileDirectory(MoveFileEntry data)
        {
            ServiceStatus = new ExecutionStatus(true);
            FileOperationResult ret = new FileOperationResult();

            APIResponse<FileOperationResult> aux
               = await _Proxys.MoveFileDirectory(data);

            SetResult<FileOperationResult>(aux, ref ret, ref ServiceStatus);

            return ret;
        }

        public async Task ListDirectories()
        {
            ServiceStatus = new ExecutionStatus(true);
            
            APIResponse<List<DirectoryResult>> aux
               = await _Proxys.ListDirectories();

            SetResult<List<DirectoryResult>>(aux, ref this.dirList, ref ServiceStatus);    
            
        }

        public async Task ListFiles(string dir)
        {
            ServiceStatus = new ExecutionStatus(true);
            List<FileResult> _list = new List<FileResult>(); 
            APIResponse<List<FileResult>> aux
               = await _Proxys.ListFiles(new FileEntry() { Directory = dir }); 

            SetResult<List<FileResult>>(aux, ref _list, ref ServiceStatus);
            this.fileList = _list.AsQueryable();
        }


        public async Task<FileOperationResult> GetFileInfo(string dir,string filename)
        {
            ServiceStatus = new ExecutionStatus(true);
            FileOperationResult ret = new FileOperationResult();

            APIResponse<FileOperationResult> aux
               = await _Proxys.GetFileInfo(new FileEntry()
               {
                   Directory =  dir,
                   Filename = filename
               });

            SetResult<FileOperationResult>(aux, ref ret, ref ServiceStatus);

          
            return ret;
        }

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                
            };

        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();

            await ListDirectories(); 
        }


        public override async Task Set()
        {           

        }

        public override async Task Remove()
        {

        }

        public override async Task Get(object id)
        {
         

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
         
        }

    }
}
