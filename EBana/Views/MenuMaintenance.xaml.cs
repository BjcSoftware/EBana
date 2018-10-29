using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Controls;

namespace EBana.Views
{
	/// <summary>
	/// Interaction logic for MenuMaintenance.xaml
	/// </summary>
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