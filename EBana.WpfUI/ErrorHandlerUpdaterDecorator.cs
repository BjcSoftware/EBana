using EBana.Domain.Commands;
using EBana.Domain.Updater;
using EBana.Domain.Updater.Exceptions;
using EBana.Excel.Core.Exceptions;
using EBana.Services.Dialog;
using System;

namespace EBana.WpfUI
{
    class ErrorHandlerUpdaterDecorator
        : ICommandService<UpdateArticles>
    {
        private readonly ICommandService<UpdateArticles> decoratedUpdater;
        private readonly IMessageBoxDialogService messageBoxService;

        public ErrorHandlerUpdaterDecorator(
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
                ShowErrorToUser(
                    "Sélectionnez d'abord un fichier Excel à partir duquel lancer la mise à jour.");
            }
            catch (FileOpenedByAnotherProcessException)
            {
                ShowErrorToUser(
                    "Le fichier sélectionné est déjà ouvert dans un autre logiciel, peut-être Excel.\n" +
                    "Fermez-le puis réessayez.");
            }
            catch (NotAnExcelFileException)
            {
                ShowErrorToUser(
                    "Le fichier sélectionné n'est pas un fichier Excel.");
            }
        }

        void ShowErrorToUser(string message)
        {
            messageBoxService.Show(
                "Erreur",
                message,
                DialogButton.Ok);
        }
    }
}
