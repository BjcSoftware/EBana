﻿using EBana.WpfUI.Core;
using EBana.WpfUI.Core.Command;
using EBana.WpfUI.Core.ViewModel;
using System;
using System.Windows.Input;

namespace EBana.WpfUI.ViewModels
{
    public class MainMenuViewModel : Notifier
    {
        public ICommand GoToCatalogueCommand { get; private set; }
        public ICommand GoToMaintenanceMenuCommand { get; private set; }

        private readonly INavigationService navigator;

        public MainMenuViewModel(INavigationService navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException(nameof(navigator));

            this.navigator = navigator;

            GoToCatalogueCommand = new RelayCommand(GoToCatalogue);
            GoToMaintenanceMenuCommand = new RelayCommand(GoToMaintenanceMenu);
        }

        private void GoToCatalogue()
        {
            navigator.NavigateTo("Catalogue");
        }

        private void GoToMaintenanceMenu()
        {
            navigator.NavigateTo("MaintenanceLogin");
        }
    }
}
