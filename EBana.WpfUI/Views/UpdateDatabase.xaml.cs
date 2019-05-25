using System.Windows.Controls;
using EBana.PresentationLogic.ViewModels;

namespace EBana.WpfUI.Views
{
	public partial class UpdateDatabase : Page
	{
		public UpdateDatabase(UpdateArticlesViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
	}
}