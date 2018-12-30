using EBana.Domain;
using EBana.Services.Dialog;
using System;

namespace EBana.WpfUI.ViewModels
{
	public class UpdateArticlesViewModel : Notifier
	{
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxDialogService messageBoxService;
        private readonly IArticleStorageUpdater updater;
        private readonly IArticleProvider articleProvider;

        public UpdateArticlesViewModel(
            IFileDialogService fileDialogService, 
            IMessageBoxDialogService messageBoxService,
            IArticleStorageUpdater updater,
            IArticleProvider articleProvider)
		{
            if (fileDialogService == null)
                throw new ArgumentNullException("fileDialogService");
            if (messageBoxService == null)
                throw new ArgumentNullException("messageBoxService");
            if (updater == null)
                throw new ArgumentNullException("updater");
            if (articleProvider == null)
                throw new ArgumentNullException("articleProvider");

            this.fileDialogService = fileDialogService;
            this.messageBoxService = messageBoxService;
            this.updater = updater;
            this.articleProvider = articleProvider;
        }
		
        public void OpenFileSelectionDialog()
        {
            UpdateSource = fileDialogService.OpenFileSelectionDialog();
        }
        
        public void TryUpdate()
        {
        	if(IsUpdateSourceCorrect())
            {
                Update();
                NotifyUserUpdateSuccessful();
            }
            else
            {
                NotifyUserUpdateFailed();
            }

            ResetUpdateSource();
        }

        private bool IsUpdateSourceCorrect()
        {
            if ((UpdateSource ?? string.Empty) != string.Empty)
                return true;
            return false;
        }

        private void Update()
        {
            var newArticles = articleProvider.GetArticles(UpdateSource);
            updater.ReplaceAvailableArticlesWith(newArticles);
        }

        private void NotifyUserUpdateSuccessful()
        {
            messageBoxService.Show(
                "Succès",
                "Mise à jour terminée.",
                DialogButton.Ok);
        }

        private void NotifyUserUpdateFailed()
        {
            messageBoxService.Show(
                "Erreur",
                "Sélectionnez d'abord un fichier Excel à partir duquel lancer la mise à jour.",
                DialogButton.Ok);
        }

        private void ResetUpdateSource()
        {
            UpdateSource = string.Empty;
        }

		#region Définition des propriétés

		private string mUpdateSource;
		public string UpdateSource
        {
			get {
				return mUpdateSource;
			}
			set {
				mUpdateSource = value;
				OnPropertyChanged("UpdateSource");
			}
		}
		
		#endregion
	}
}
