using EBana.WpfUI.ViewModels;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class MainMenu : Page
	{
		public MainMenu(MainMenuViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
	}
}