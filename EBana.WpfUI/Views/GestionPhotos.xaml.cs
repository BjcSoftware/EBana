using EBana.WpfUI.ViewModels;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class GestionPhotos : Page
	{
		public GestionPhotos(GestionPhotosViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
    }
}