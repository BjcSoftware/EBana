using System.Windows.Controls;
using System.Windows.Input;
using EBana.Security;
using OsServices.Dialog;

namespace EBana.ViewModels
{
    class NouveauMotDePasseViewModel : Notifier
    {
        public ICommand NewPasswordCommand { get; private set; }

        private PasswordBox txtCurrentPassword;
        private PasswordBox txtNewPassword;
        private PasswordBox txtNewPasswordConfirmation;

        private readonly IMessageBoxService messageBoxService;
        private readonly Authenticator authenticator;
        private readonly PasswordUpdater passwordUpdater;

        public NouveauMotDePasseViewModel(
            IMessageBoxService messageBoxService, 
            Authenticator authenticator, 
            PasswordUpdater passwordUpdater)
        {
            this.messageBoxService = messageBoxService;
            this.authenticator = authenticator;
            this.passwordUpdater = passwordUpdater;

            NewPasswordCommand = new RelayParameterizedCommand((param) => NewPassword(param as object[]));
        }

        private void NewPassword(object[] passwordBoxes)
        {
            BindPasswordBoxes(passwordBoxes);

            if(AreSuppliedInformationsCorrect())
            {
                DefineNewPassword();
                NotifyUserThatPasswordSuccessfullyChanged();
            }
            else
            {
                NotifyUserThatPasswordDidNotChanged();
            }

            ClearPasswordBoxes();
        }

        private void BindPasswordBoxes(object[] passwordBoxes)
        {
            // Les PasswordBoxes sont difficiles à gérer, elles sont donc gérées à part 
            // (leurs valeurs ne peuvent pas être bind à une propriété)
            txtCurrentPassword = passwordBoxes[0] as PasswordBox;
            txtNewPassword = passwordBoxes[1] as PasswordBox;
            txtNewPasswordConfirmation = passwordBoxes[2] as PasswordBox;
        }

        private bool AreSuppliedInformationsCorrect()
        {
            return AreNewPasswordAndConfirmationTheSame() && IsPasswordCorrect();
        }

        private bool AreNewPasswordAndConfirmationTheSame()
        {
            return txtNewPassword.Password == txtNewPasswordConfirmation.Password;
        }

        private bool IsPasswordCorrect()
        {
            return authenticator.IsPasswordCorrect(txtCurrentPassword.Password);
        }

        private void DefineNewPassword()
        {
            string newPassword = txtNewPassword.Password;
            passwordUpdater.Update(newPassword);
        }

        private void NotifyUserThatPasswordSuccessfullyChanged()
        {
            messageBoxService.Show("Succès",
                        "Le mot de passe a été mis à jour.",
                        System.Windows.MessageBoxButton.OK);
        }

        private void NotifyUserThatPasswordDidNotChanged()
        {
            messageBoxService.Show("Erreur",
                            "Les informations saisies sont incorrectes: les mots de passe ne correspondent pas ou le mot de passe actuel saisit n'est pas correct.",
                            System.Windows.MessageBoxButton.OK);
        }

        private void ClearPasswordBoxes()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtNewPasswordConfirmation.Clear();
        }
    }
}