﻿//using Azure.Storage.Blobs;
//using Azure.Storage.Blobs.Models;
using FullDevToolKit.Common;


namespace MyApp.API
{

    public interface IFileService
    {
        Task<ExecutionStatus> UploadFile(Stream content, string filename);
        Stream DownloadFile(string filename); 

    }

    public class AzureFileService: IFileService
    {

        //private BlobClient _service = null;

        private string _connection = "";
        private string _container = ""; 

        public AzureFileService(string connection, string container)
        {
            _connection = connection;   
            _container = container; 
           

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
