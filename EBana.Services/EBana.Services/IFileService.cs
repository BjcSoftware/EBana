using System.Collections.Generic;

namespace EBana.Services.File
{
	public interface IFileService
	{
	    bool DirectoryExists(string path);
		IEnumerable<string> GetAllFileNamesInFolder(string folderPath);
        void Copy(string sourceFileName, string destFileName);
	    void CreateDirectory(string path);
	}
}
