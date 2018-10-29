using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EBana.Views
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Page
	{
		public MainMenu()
		{
			InitializeComponent();
		}
		
		void cmdConsulter_Click(object sender, RoutedEventArgs e)
		{
            BBCodeBlock bs = new BBCodeBlock();
			bs.LinkNavigator.Navigate(new Uri("/Views/ConsultationCatalogue.xaml",
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