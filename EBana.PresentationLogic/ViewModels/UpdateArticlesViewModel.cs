using EBana.Domain;
using EBana.Domain.ArticleStorageUpdater;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.Services.Dialog;
using System;

namespace EBana.PresentationLogic.ViewModels
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
                throw new ArgumentNullException(nameof(fileDialogService));
            if (messageBoxService == null)
                throw new ArgumentNullException(nameof(messageBoxService));
            if (updater == null)
                throw new ArgumentNullException(nameof(updater));
            if (articleProvider == null)
                throw new ArgumentNullException(nameof(articleProvider));

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
            var newArticles = articleProvider.GetArticlesFrom(UpdateSource);
            updater.ReplaceAvailableArticlesWith(newArticles);
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
