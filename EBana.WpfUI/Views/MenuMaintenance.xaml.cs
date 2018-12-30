using System;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class MenuMaintenance : Page
	{
		public MenuMaintenance()
		{
			InitializeComponent();
		}
		
		void cmdUpdate_click(object sender, RoutedEventArgs e)
		{
			BBCodeBlock bs = new BBCodeBlock();
			bs.LinkNavigator.Navigate(new Uri("/Views/UpdateDatabase.xaml",
			                                   UriKind.Relative), this);
		}

        private void cmdNewPassword_Click(object sender, RoutedEventArgs e)
        {
            BBCodeBlock bs = new BBCodeBlock();
            bs.LinkNavigator.Navigate(new Uri("/Views/NouveauMotDePasse.xaml",
                                               UriKind.Relative), this);
        }
		
		void cmdPhotos_Click(object sender, RoutedEventArgs e)
		{
			BBCodeBlock bs = new BBCodeBlock();
            bs.LinkNavigator.Navigate(new Uri("/Views/GestionPhotos.xaml",
                                               UriKind.Relative), this);
		}
    }
}