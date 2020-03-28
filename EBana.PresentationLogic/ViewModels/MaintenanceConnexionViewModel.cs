using System.Windows.Controls;
using System.Windows.Input;
using System;
using EBana.Services.Dialog;
using EBana.Domain.Security;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.PresentationLogic.Core;
using EBana.PresentationLogic.Core.Command;
using EBana.Domain.Models;

namespace EBana.PresentationLogic.ViewModels
{
	public class MaintenanceConnexionViewModel : Notifier
	{
        public ICommand LoginCommand { get; private set; }

        private PasswordBox passwordBox;

        private readonly IMessageBoxDialogService messageBoxService;
        private readonly IAuthenticator authenticator;
        private readonly INavigationService navigator;

        public MaintenanceConnexionViewModel(
            IMessageBoxDialogService messageBoxService, 
            IAuthenticator authenticator,
            INavigationService navigator)
		{
            if (messageBoxService == null)
                throw new ArgumentNullException(nameof(messageBoxService));
            if (authenticator == null)
                throw new ArgumentNullException(nameof(authenticator));
            if (navigator == null)
                throw new ArgumentNullException(nameof(navigator));

            this.messageBoxService = messageBoxService;
            this.authenticator = authenticator;
            this.navigator = navigator;

            LoginCommand = new RelayParameterizedCommand( (param) => Login(param) );
		}
		
		private void Login(object passwordBox)
		{
            BindPasswordBox(passwordBox);

            if (PasswordIsCorrect())
            {
                OpenMaintenanceMainMenu();
            }
            else
            {
                NotifyUserPasswordNotCorrect();
            }

            this.passwordBox.Clear();
		}

        private void BindPasswordBox(object passwordBox)
        {
            // La PasswordBox est difficile à gérer, elle est donc gérée à part 
            // (sa valeur ne peut pas être bind à une propriété)
            this.passwordBox = passwordBox as PasswordBox;
        }

        private bool PasswordIsCorrect()
        {
            return authenticator
                .IsPasswordCorrect(
                    new UnhashedPassword(passwordBox.Password));
        }

        private void OpenMaintenanceMainMenu()
        {
            navigator.NavigateTo("MaintenanceMenu");
        }

        private void NotifyUserPasswordNotCorrect()
        {
            messageBoxService.Show(
                "Erreur",
                "Le mot de passe saisit n'est pas valide.",
                DialogButton.Ok);
        }
	}
}