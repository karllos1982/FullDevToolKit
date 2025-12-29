using Azure;
using FullDevToolKit.Common;
using System.IO;
using FullDevToolKit.Core.Common;

namespace MyApp.API
{
    
    public class LocalFileService : IFileService
    {

        private string _connection = "";
        private string _basepath= "";

        public LocalFileService()
        {
            
        }

        public void Init(string connection)
        {
            _connection = connection;

            _basepath = $@"{Environment.CurrentDirectory}";
            _basepath = _basepath.Replace('\\', '/');
            _basepath = _basepath + $"/FileServer/";
        }

        public async Task<FileOperationResult> UploadFile(Stream content, string directory,
            string filename)
        {

            FileOperationResult ret = new FileOperationResult();

            try
            {
                             
                string fisicalpath = "";
                fisicalpath = $"{_basepath}{directory}/{filename}";

                if (File.Exists(fisicalpath))
                {
                    File.Delete(fisicalpath);
                }

                MemoryStream aux = new MemoryStream();
                await content.CopyToAsync(aux);
                File.WriteAllBytes(fisicalpath, aux.ToArray());
                ret.Status = true;

            }
            catch (Exception ex)
            {                
                ret.ErrMessage = ex.Message;
            }

            return ret;

        }

        public Stream DownloadFile(string directory, string filename)
        {

            Stream ret = null;

            try
            {

                string fisicalpath = "";                
                fisicalpath = $"{_basepath}{directory}/{filename}";

                if (File.Exists(fisicalpath))
                {
                    ret = new FileStream(fisicalpath, FileMode.Open);
                }
            }
            catch (Exception ex)
            {

            }

            return ret;

        }

        public async Task<FileOperationResult> DeleteFile(string directory, string filename)
        {
            FileOperationResult ret = new FileOperationResult();

            try
            {

                string fisicalpath = "";             
                fisicalpath = $"{_basepath}{directory}/{filename}";

                if (File.Exists(fisicalpath))
                {
                    File.Delete(fisicalpath);
                    ret.Status = true;
                }
            }
            catch (Exception ex)
            {                
                ret.ErrMessage = ex.Message;
            }

            return ret;
        }


        public async Task<FileOperationResult> MoveFileDirectory(MoveFileEntry param)           
        {
            FileOperationResult ret = new FileOperationResult();

            try
            {

                string fisicalpath = "";
                fisicalpath = $"{_basepath}{param.FromDirectory}/{param.Filename}";

                if (File.Exists(fisicalpath))
                {
                    string newpath = $"{_basepath}{param.ToDirectory}/{param.Filename}"; 

                    if (!File.Exists(newpath))
                    {
                        Directory.Move(fisicalpath, newpath);
                        ret.Status = true; 
                    }
                    else
                    {                        
                        ret.ErrMessage =  "Já existe um arquivo no destino com o mesmo nome.";
                    }
                    
                }
                else
                {                    
                    ret.ErrMessage = "O arquivo de origem não existe.";
                }
            }
            catch (Exception ex)
            {
                ret.ErrMessage = ex.Message;
            }

            return ret;
        }

        public async Task<List<DirectoryResult>> ListDirectories()
        {
            List<DirectoryResult> ret = new List<DirectoryResult>();

            if (Directory.Exists(_basepath))
            {
                var dirs = Directory.GetDirectories(_basepath);
                foreach (var dir in dirs)
                {
                    ret.Add(new DirectoryResult() { Directory = Path.GetFileName(dir) });
                }
            }

            return ret;
        }

        public async Task<List<FileListResult>> ListFiles(string directory)
        {
            List<FileListResult> ret = new List<FileListResult>();

            string fisicalpath = $"{_basepath}{directory}/";

            if (Directory.Exists(fisicalpath))
            {
                var files = Directory.GetFiles(fisicalpath);
                foreach (var file in files)
                {
                    ret.Add( new FileListResult() 
                    { 
                        Filename = Path.GetFileName(file),  
                        Directory = directory
                    });
                }
            }

            return ret;
        }

        public async Task<FileOperationResult> GetFileInfo(string directory, string filename)
        {
            FileOperationResult ret = new FileOperationResult();

            string fisicalpath = "";
            fisicalpath = $"{_basepath}{directory}/{filename}";

            if (File.Exists(fisicalpath))
            {

                FileInfo info = new FileInfo(fisicalpath);
                ret.Status = true;
                ret.Info = info; 

            }
            else
            {                
                ret.ErrMessage = "O arquivo não existe.";
            }

            return ret;
        }

        public string GetFileURL(string directory, string filename)
        {
            string ret = "";

            return ret;
        }
    }


    public class AzureFileService
    {

        //private BlobClient _service = null;

        private string _connection = "";
        private string _container = ""; 

        public AzureFileService(string connection)
        {
            _connection = connection;              
           
        }

        public async Task<ExecutionStatus> UploadFile(Stream content, string filename)
        {

            ExecutionStatus ret = new ExecutionStatus(true);

            try
            {
                //_service = new BlobClient(_connection, _container,filename);

                //BlobContentInfo info = null;

                //info = await _service.UploadAsync(content);
                //ret.Returns = _service.Uri.AbsoluteUri;
                
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.Exceptions.AddException("Error",ex.Message); 
            }
            
            return ret;

        }

        public Stream DownloadFile(string filename)
        {

            Stream ret = null; 

            try
            {
                //_service = new BlobClient(_connection, _container, filename);
                
                //BlobOpenReadOptions opts = new BlobOpenReadOptions(false);
             
                //if (!_service.Exists())
                //{                                   
                //    _service = new BlobClient(_connection, _container, "user_anonymous.png");                    
                //}

                //ret = _service.OpenRead(opts);
                

            }
            catch (Exception ex)
            {
            
            }


            return ret;

        }

                  

    }


}
