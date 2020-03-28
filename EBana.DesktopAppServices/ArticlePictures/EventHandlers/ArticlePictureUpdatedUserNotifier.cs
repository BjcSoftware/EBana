using EBana.Domain;
using EBana.Domain.ArticlePictures.Events;
using EBana.Services.Dialog;
using System;

namespace EBana.DesktopAppServices.ArticlePictures.EventHandlers
{
    public class ArticlePictureUpdatedUserNotifier 
        : IEventHandler<ArticlePictureUpdated>
    {
        private readonly IMessageBoxDialogService dialogService;

        public ArticlePictureUpdatedUserNotifier(
            IMessageBoxDialogService dialogService)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            this.dialogService = dialogService;
        }

        public void Handle(ArticlePictureUpdated e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            dialogService.Show(
                "Succès",
                $"La photo de l'article {e.Article.Reference} ({e.Article.Libelle}) a été mise à jour.",
                DialogButton.Ok);
        }
    }
}
