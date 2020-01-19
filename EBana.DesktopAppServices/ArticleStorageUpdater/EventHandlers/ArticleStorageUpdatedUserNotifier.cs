using EBana.Domain;
using EBana.Domain.ArticleStorageUpdater.Event;
using EBana.Services.Dialog;
using System;

namespace EBana.DesktopAppServices.ArticleStorageUpdater.EventHandlers
{
    public class ArticleStorageUpdatedUserNotifier 
        : IEventHandler<ArticleStorageUpdated>
    {
        private readonly IMessageBoxDialogService dialogService;

        public ArticleStorageUpdatedUserNotifier(
            IMessageBoxDialogService dialogService)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            this.dialogService = dialogService;
        }

        public void Handle(ArticleStorageUpdated e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            NotifyUserArticleStorageUpdated();
        }

        private void NotifyUserArticleStorageUpdated()
        {
            dialogService.Show(
                "Succès",
                "Mise à jour terminée.",
                DialogButton.Ok);
        }
    }
}
