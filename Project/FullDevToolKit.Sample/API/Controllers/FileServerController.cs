using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Common;
using MyApp.Models;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FileServerController : APIControllerBase
    {
        IFileService _fs = null;

       public FileServerController(IContext context)
       {
            Init(context, null, "");
            _fs = new LocalFileService(""); 
       }

        [HttpPost]
        [Route("uploadfile")]
        [Authorize]
        public async Task<object> UploadFile(FileEntry param)
        {
            Stream body = Request.Body;
            
             return await _fs.UploadFile(body, param.Directory, param.Filename);
            
        }

        [HttpGet]
        [Route("downloadfile")]
        public FileStreamResult DownloadFile(string dir, string file)
        {
            FileStreamResult result = null;
            Stream str = _fs.DownloadFile(dir, file);

            if (str != null)
            {
                result =  new FileStreamResult(str, "application/octet-stream");
            }            

            return result;
        }

        [HttpPost]
        [Route("deletefile")]
        [Authorize]
        public async Task<object> DeleteFile(FileEntry param)
        {

            return await _fs.DeleteFile(param.Directory,param.Filename);

        }


        [HttpPost]
        [Route("movefiledirectory")]
        [Authorize]
        public async Task<object> MoveFileDirectory(MoveFileEntry param)
        {
            
            return await _fs.MoveFileDirectory(param);
            
        }

        [HttpGet]
        [Route("listdirectories")]
        public async Task<object> ListDirectories()
        {
           ret = await _fs.ListDirectories();   
           return ret;
        }

        [HttpPost]
        [Route("listfiles")]
        public async Task<object> ListFiles(FileEntry param)
        {
            ret = await _fs.ListFiles(param.Directory); 
            return ret;
        }

        [HttpPost]
        [Route("getfileinfo")]
        public async Task<object> GetFileInfo(FileEntry param)
        {
            ret = await _fs.GetFileInfo(param.Directory, param.Filename);
            return ret;
        }

    }
}
