using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class MainMenu : Page
	{
		public MainMenu()
		{
			InitializeComponent();
		}
		
		void cmdConsulter_Click(object sender, RoutedEventArgs e)
		{
            BBCodeBlock bs = new BBCodeBlock();
			bs.LinkNavigator.Navigate(new Uri("/Views/Catalogue.xaml",
			                                   UriKind.Relative), this);
		}
		
		void cmdMaintenance_Click(object sender, RoutedEventArgs e)
		{
			BBCodeBlock bs = new BBCodeBlock();
			bs.LinkNavigator.Navigate(new Uri("/Views/MaintenanceConnexion.xaml",
			                                   UriKind.Relative), this);
		}
	}
}