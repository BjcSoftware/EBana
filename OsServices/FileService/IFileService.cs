using System.Collections.Generic;

namespace OsServices.File
{
	public interface IFileService
	{
	    bool DirectoryExists(string path);
		IEnumerable<string> GetAllFileNamesInFolder(string folderPath);
        void Copy(string sourceFileName, string destFileName);
	    void CreateDirectory(string path);
	}
}
