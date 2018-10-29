using System.Collections.Generic;

namespace OsServices.File
{
	public interface IFileService
	{
		IEnumerable<string> GetAllFileNamesInFolder(string folderPath);
        void Copy(string sourceFileName, string destFileName);
    }
}
