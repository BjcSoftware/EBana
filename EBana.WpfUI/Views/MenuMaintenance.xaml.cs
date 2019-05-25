using System.Windows.Controls;
using EBana.PresentationLogic.ViewModels;

namespace EBana.WpfUI.Views
{
	public partial class MenuMaintenance : Page
	{
		public MenuMaintenance(MaintenanceMenuViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
    }
}