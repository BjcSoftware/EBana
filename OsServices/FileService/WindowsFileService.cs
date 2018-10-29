using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace OsServices.File
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
	}
}
