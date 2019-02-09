using System;
using System.Windows.Controls;
using System.Windows.Input;
using EBana.Domain.Security;
using EBana.Services.Dialog;

namespace EBana.WpfUI.ViewModels
{
    class NouveauMotDePasseViewModel : Notifier
    {
        public ICommand NewPasswordCommand { get; private set; }

        private PasswordBox txtCurrentPassword;
        private PasswordBox txtNewPassword;
        private PasswordBox txtNewPasswordConfirmation;

        private readonly IMessageBoxDialogService messageBoxService;
        private readonly IAuthenticator authenticator;
        private readonly IPasswordUpdater passwordUpdater;

        public NouveauMotDePasseViewModel(
            IMessageBoxDialogService messageBoxService,
            IAuthenticator authenticator,
            IPasswordUpdater passwordUpdater)
        {
            if (messageBoxService == null)
                throw new ArgumentNullException("messageBoxService");
            if (authenticator == null)
                throw new ArgumentNullException("authenticator");
            if (passwordUpdater == null)
                throw new ArgumentNullException("passwordUpdater");

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
            messageBoxService.Show(
                "Succès",
                "Le mot de passe a été mis à jour.",
                DialogButton.Ok);
        }

        private void NotifyUserThatPasswordDidNotChanged()
        {
            messageBoxService.Show(
                "Erreur",
                "Les informations saisies sont incorrectes: les mots de passe ne correspondent pas ou le mot de passe actuel saisit n'est pas correct.",
                DialogButton.Ok);
        }

        private void ClearPasswordBoxes()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtNewPasswordConfirmation.Clear();
        }
    }
}