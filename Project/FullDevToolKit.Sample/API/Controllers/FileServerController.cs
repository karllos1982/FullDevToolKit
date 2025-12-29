using API.Code;
using FullDevToolKit; 
using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.API;
using MyApp.Models;
using System.Buffers;
using static Dapper.SqlMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]   
    public class FileServerController : APIControllerBase
    {
       
       public FileServerController(IContext context, IFileService fileService)
       {
            Init(context, null, "");

            this.FileServer = fileService;
            this.FileServer.Init("");
        }

        [HttpPost]
        [Route("uploadfile")]
        [Authorize]
        public async Task<object> UploadFile(string pDirectory, string pFileName)
        {

            Stream body = Request.Body;

            FileOperationResult data
              =  await this.FileServer.UploadFile(body, pDirectory, pFileName);

            ret = SetReturn(data);
            return ret;

        }

        [HttpGet]
        [Route("downloadfile")]
        public FileStreamResult DownloadFile(string dir, string file)
        {
            FileStreamResult result = null;
            Stream str = this.FileServer.DownloadFile(dir, file);

            if (str != null)
            {               
                result =  new FileStreamResult(str, FileHelper.GetContentType(file));
            }            

            return result;
        }

        [HttpPost]
        [Route("deletefile")]
        [Authorize]
        public async Task<object> DeleteFile(FileEntry param)
        {

            FileOperationResult data
                 = await this.FileServer.DeleteFile(param.Directory,param.Filename);

            ret = SetReturn(data);
            return ret;

        }


        [HttpPost]
        [Route("movefiledirectory")]
        [Authorize]
        public async Task<object> MoveFileDirectory(MoveFileEntry param)
        {
            
            FileOperationResult data
                = await this.FileServer.MoveFileDirectory(param);

            ret = SetReturn(data); 

            return ret;
        }

        [HttpGet]
        [Route("listdirectories")]
        public async Task<object> ListDirectories()
        {
           List<DirectoryResult> data = await this.FileServer.ListDirectories();
            ret = SetReturn(data); 
           return ret;
        }

        [HttpGet]
        [Route("listfiles")]
        public async Task<object> ListFiles(string pDirectory)
        {
            
            List<FileListResult> data 
                = await this.FileServer.ListFiles(pDirectory);

            ret = SetReturn(data);
            return ret;
        }

        [HttpPost]
        [Route("getfileinfo")]
        public async Task<object> GetFileInfo(FileEntry param)
        {
            FileOperationResult data
                    = await this.FileServer.GetFileInfo(param.Directory, param.Filename);
            
            ret = SetReturn(data);
            return ret;
        }

    }
}
