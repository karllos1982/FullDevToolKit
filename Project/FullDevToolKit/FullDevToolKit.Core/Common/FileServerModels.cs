using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Core.Common
{

    public class FileEntry
    {
        public string Directory { get; set; }
        public string Filename { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class FileResult
    {
        public string Directory { get; set; }
        public string Filename { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class DirectoryResult
    {
        public string Directory { get; set; }  
    }

    public class MoveFileEntry
    {
        public string Filename { get; set; }
        public string FromDirectory { get; set; }
        public string ToDirectory { get; set; }

    }

    public class FileOperationResult
    {
        public FileOperationResult()
        {

        }

        public string Filename { get; set; } = "";

        public bool Status { get; set; } = false;

        public string ErrMessage { get; set; } = "";

        public FileInfo Info { get; set; } = null;
    }

    public interface IFileService
    {
        Task<FileOperationResult> UploadFile(Stream content, string directory, string filename);

        Stream DownloadFile(string directory, string filename);

        Task<FileOperationResult> DeleteFile(string directory, string filename);

        Task<FileOperationResult> MoveFileDirectory(MoveFileEntry param);

        Task<List<DirectoryResult>> ListDirectories();

        Task<List<FileResult>> ListFiles(string directory);

        Task<FileOperationResult> GetFileInfo(string directory, string filename);

        string GetFileURL(string directory, string filename);
    }

}
