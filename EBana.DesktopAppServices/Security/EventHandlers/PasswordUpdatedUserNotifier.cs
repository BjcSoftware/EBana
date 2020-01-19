using EBana.Domain;
using EBana.Domain.Security.Event;
using EBana.Services.Dialog;
using System;

namespace EBana.DesktopAppServices.Security.EventHandlers
{
    public class PasswordUpdatedUserNotifier : IEventHandler<PasswordUpdated>
    {
        private readonly IMessageBoxDialogService dialogService;

        public PasswordUpdatedUserNotifier(IMessageBoxDialogService dialogService)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            this.dialogService = dialogService;
        }

        public void Handle(PasswordUpdated e)
        {
            NotifyUserThatPasswordChanged();
        }

        private void NotifyUserThatPasswordChanged()
        {
            dialogService.Show(
                "Succès",
                "Le mot de passe a été mis à jour.",
                DialogButton.Ok);
        }
    }
}
