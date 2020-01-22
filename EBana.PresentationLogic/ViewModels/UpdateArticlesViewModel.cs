using EBana.Domain.Commands;
using EBana.Domain.Updater;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.Services.Dialog;
using System;

namespace EBana.PresentationLogic.ViewModels
{
	public class UpdateArticlesViewModel : Notifier
	{
        private readonly ICommandService<UpdateArticles> updater;
        private readonly IFileDialogService fileDialogService;

        public UpdateArticlesViewModel(
            ICommandService<UpdateArticles> updater,
            IFileDialogService fileDialogService)
		{
            if (updater == null)
                throw new ArgumentNullException(nameof(updater));
            if (fileDialogService == null)
                throw new ArgumentNullException(nameof(fileDialogService));

            this.updater = updater;
            this.fileDialogService = fileDialogService;
        }
		
        public void OpenFileSelectionDialog()
        {
            UpdateSource = fileDialogService.OpenFileSelectionDialog();
        }
        
        public void TryUpdate()
        {
            updater.Execute(new UpdateArticles(UpdateSource));

            ResetUpdateSource();
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
