using EBana.PresentationLogic.Core;
using EBana.PresentationLogic.Core.Command;
using EBana.PresentationLogic.Core.ViewModel;
using System;
using System.Windows.Input;

namespace EBana.PresentationLogic.ViewModels
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
