using EBana.Domain.Commands;
using EBana.Domain.Updater;
using EBana.Domain.Updater.Exceptions;
using EBana.Services.Dialog;
using System;

namespace EBana.WpfUI
{
    class InvalidUpdateSourceUserNotifier
        : ICommandService<UpdateArticles>
    {
        private readonly ICommandService<UpdateArticles> decoratedUpdater;
        private readonly IMessageBoxDialogService messageBoxService;

        public InvalidUpdateSourceUserNotifier(
            ICommandService<UpdateArticles> decoratedUpdater,
            IMessageBoxDialogService messageBoxService)
        {
            if (decoratedUpdater == null)
                throw new ArgumentNullException(nameof(decoratedUpdater));
            if (messageBoxService == null)
                throw new ArgumentNullException(nameof(messageBoxService));

            this.decoratedUpdater = decoratedUpdater;
            this.messageBoxService = messageBoxService;
        }

        public void Execute(UpdateArticles command)
        {
            try
            {
                decoratedUpdater.Execute(command);
            }
            catch (InvalidUpdateSourceException)
            {
                messageBoxService.Show(
                    "Erreur",
                    "Sélectionnez d'abord un fichier Excel à partir duquel lancer la mise à jour.",
                    DialogButton.Ok);
            }
        }
    }
}
