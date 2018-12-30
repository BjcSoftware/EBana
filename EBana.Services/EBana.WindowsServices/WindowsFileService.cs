using System.IO;
using System.Linq;
using System.Collections.Generic;
using EBana.Services.File;

namespace EBana.WindowsServices.File
{
	public class WindowsFileService : IFileService
	{
        public IEnumerable<string> GetAllFileNamesInFolder(string folderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            return dirInfo.GetFiles().Select(f => f.Name);
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            System.IO.File.Copy(sourceFileName, destFileName, overwrite: true);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
