﻿using EBana.PresentationLogic.ViewModels;
using System.Windows.Controls;

namespace EBana.WpfUI.Views
{
	public partial class MaintenanceConnexion : Page
	{
		public MaintenanceConnexion(MaintenanceConnexionViewModel vm)
		{
			InitializeComponent();
            DataContext = vm;
		}
    }
}