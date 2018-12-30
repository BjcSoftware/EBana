namespace EBana.Services.Dialog
{
    public interface IFileDialogService
    {
        string OpenFileSelectionDialog();
        void OpenFileBrowserInFolder(string folderPath);

        string Filter { get; set; }
    }
}
