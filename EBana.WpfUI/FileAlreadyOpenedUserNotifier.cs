using EBana.Domain.Commands;
using EBana.Domain.Updater;
using System;
using EBana.Excel.Core.Exceptions;
using EBana.Services.Dialog;

namespace EBana.WpfUI
{
    class FileAlreadyOpenedUserNotifier :
        ICommandService<UpdateArticles>
    {
        private readonly ICommandService<UpdateArticles> decoratedUpdater;
        private readonly IMessageBoxDialogService messageBoxService;

        public FileAlreadyOpenedUserNotifier(
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
            catch (FileOpenedByAnotherProcessException)
            {
                messageBoxService.Show(
                    "Erreur",
                    "Le fichier sélectionné est déjà ouvert dans un autre logiciel, peut-être Excel.\n" +
                    "Fermez-le puis réessayez.",
                    DialogButton.Ok);
            }
        }
    }
}
