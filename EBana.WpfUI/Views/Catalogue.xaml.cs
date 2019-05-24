using EBana.WpfUI.ViewModels;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class Catalogue : Page
	{
		public Catalogue(CatalogueViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
    }
}