using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.ApplicationHelpers;
using FullDevToolKit.Core.Common;
using MyApp.Models;
using Newtonsoft.Json;
using System.IO.Enumeration;


namespace MyApp.Proxys
{
    public interface IFileServerProxyManager
    {
        void Init(HttpClient http, string baseurl, string token);

    }

    public class FileServerProxy : APIProxyBase, IFileServerProxyManager
    {


        public FileServerProxy()
        {

        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            this.InitializeAPI(http, baseurl + "/fileserver/", token);

        }

        public async Task<APIResponse<FileOperationResult>> UploadFile(FileEntry data)
        {
            APIResponse<FileOperationResult> ret = null;

            ret = await PostAsJSON<FileOperationResult>("uploadfile", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        
        public async Task<APIResponse<FileOperationResult>> DeleteFile(FileEntry data)
        {
            APIResponse<FileOperationResult> ret = null;

            ret = await PostAsJSON<FileOperationResult>("deletefile", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<FileOperationResult>> MoveFileDirectory(MoveFileEntry data)
        {
            APIResponse<FileOperationResult> ret = null;

            ret = await PostAsJSON<FileOperationResult>("movefiledirectory", JsonConvert.SerializeObject(data), null);

            return ret;
        }

        public async Task<APIResponse<List<DirectoryResult>>> ListDirectories()
        {
            APIResponse<List<DirectoryResult>> ret = null;
            
            ret = await GetAsJSON<List<DirectoryResult>>("listdirectories", null);

            return ret;
        }

        public async Task<APIResponse<List<FileListResult>>> ListFiles(FileParam param)
        {
            APIResponse<List<FileListResult>> ret = null;

            object[] p= new object[1];
            p[0] = param;

            ret = await GetAsJSON<List<FileListResult>>("listfiles", p);

            return ret;
        }


        public async Task<APIResponse<FileOperationResult>> GetFileInfo(FileEntry data)
        {
            APIResponse<FileOperationResult> ret = null;

            ret = await PostAsJSON<FileOperationResult>("getfileinfo", JsonConvert.SerializeObject(data), null);

            return ret;
        }



    }

}
