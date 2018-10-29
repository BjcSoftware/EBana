using EBana.ViewModels;
using OsServices.Dialog;
using System.Windows.Controls;

namespace EBana.Views
{
	public partial class UpdateDatabase : Page
	{
		public UpdateDatabase()
		{
			InitializeComponent();
            DataContext = CreateViewModel();
		}

        private UpdateDatabaseViewModel CreateViewModel()
        {
            IFileDialogService fileDialogService = new WindowsFileDialogService
            {
                Filter = "Fichiers Excel (*.xlsx;*.xls)|*xlsx;*xls|Tous les fichiers (*.*)|*.*"
            };

            IMessageBoxService messageBoxService = new MessageBoxService();

            return new UpdateDatabaseViewModel(fileDialogService, messageBoxService);
        }
	}
}