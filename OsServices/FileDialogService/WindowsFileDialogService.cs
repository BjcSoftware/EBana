using Microsoft.Win32;

namespace OsServices.Dialog
{
    public class WindowsFileDialogService : IFileDialogService
    {
        public string Filter { get; set; }

        public string OpenFileSelectionDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Filter
            };

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            return null;
        }

        public void OpenFileBrowserInFolder(string folderPath)
        {
            System.Diagnostics.Process.Start(folderPath.Replace('/', '\\'));
        }
    }
}
