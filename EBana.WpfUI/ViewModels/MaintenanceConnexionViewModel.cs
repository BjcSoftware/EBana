using System.Windows.Controls;
using System.Windows.Input;
using EBana.Security;
using System;
using EBana.Services.Dialog;
using EBana.Domain.Security;

namespace EBana.WpfUI.ViewModels
{
	public class MaintenanceConnexionViewModel : Notifier
	{
        public ICommand LoginCommand { get; private set; }

        private PasswordBox passwordBox;

        private readonly IMessageBoxDialogService messageBoxService;
        private readonly IAuthenticator authenticator;

        public MaintenanceConnexionViewModel(
            IMessageBoxDialogService messageBoxService, 
            IAuthenticator authenticator)
		{
            if (messageBoxService == null)
                throw new ArgumentNullException("messageBoxService");
            if (authenticator == null)
                throw new ArgumentNullException("authenticator");

            this.messageBoxService = messageBoxService;
            this.authenticator = authenticator;

            LoginCommand = new RelayParameterizedCommand( (param) => DoLoginCommand(param) );
		}
		
		private void DoLoginCommand(object passwordBox)
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
            return authenticator.IsPasswordCorrect(passwordBox.Password);
        }

        private void OpenMaintenanceMainMenu()
        {
            System.Windows.IInputElement target = FirstFloor.ModernUI.Windows.Navigation.NavigationHelper.FindFrame(
                "_top", 
                System.Windows.Application.Current.MainWindow);

            NavigationCommands.GoToPage.Execute("/Views/MenuMaintenance.xaml", target);
        }

        private void NotifyUserPasswordNotCorrect()
        {
            messageBoxService.Show("Erreur",
                    "Le mot de passe saisit n'est pas valide.",
                    DialogButton.Ok);
        }
	}
}