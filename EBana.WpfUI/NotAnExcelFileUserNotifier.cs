using EBana.Domain.Commands;
using EBana.Domain.Updater;
using EBana.Excel.Core.Exceptions;
using EBana.Services.Dialog;
using System;

namespace EBana.WpfUI
{
    public class NotAnExcelFileUserNotifier
        : ICommandService<UpdateArticles>
    {
        private readonly ICommandService<UpdateArticles> decoratedUpdater;
        private readonly IMessageBoxDialogService messageBoxService;

        public NotAnExcelFileUserNotifier(
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
            catch (NotAnExcelFileException)
            {
                messageBoxService.Show(
                    "Erreur",
                    "Le fichier sélectionné n'est pas un fichier Excel",
                    DialogButton.Ok);
            }
        }
    }
}
