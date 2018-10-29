using System;
using System.Data.Entity;
using Data.Repository;
using EBana.Factory;
using EBana.Models;
using OsServices.Dialog;

namespace EBana.ViewModels
{
	public class UpdateDatabaseViewModel : Notifier
	{
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        public UpdateDatabaseViewModel(IFileDialogService fileDialogService, IMessageBoxService messageBoxService)
		{
            this.fileDialogService = fileDialogService;
            this.messageBoxService = messageBoxService;
        }
		
        public void OpenFileSelectionDialog()
        {
        	if(fileDialogService == null) throw new NullReferenceException("FileDialogService");
        	
            ExcelFilePath = fileDialogService.OpenFileSelectionDialog();
        }
        
        public void Update()
        {
        	if(IsUpdateSourceCorrect())
        	{
                ArticleStorageUpdater updater = CreateUpdater();
                updater.Update();

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
        	if((ExcelFilePath ?? string.Empty) != string.Empty)
        		return true;
        	return false;
        }
        
        private ArticleStorageUpdater CreateUpdater()
        {
            IArticleFactory articleFactory = CreateArticleFactory();

            DbContext context = new EBanaContext();
            IWriter<Article> articleWriter = new EfWriter<Article>(context);
            IWriter<TypeEpi> typeEpiWriter = new EfWriter<TypeEpi>(context);

            return new ArticleStorageUpdater(articleFactory, articleWriter, typeEpiWriter);
        }

        private IArticleFactory CreateArticleFactory()
        {
            IRawArticleFactory rawArticleFactory = new RawArticleExcelFactory(ExcelFilePath);
            IArticleFactory articleFactory = new ArticleFromRawArticleFactory(rawArticleFactory);
            return articleFactory;
        }

        private void NotifyUserUpdateSuccessful()
        {
            messageBoxService.Show("Succès",
                    "Mise à jour terminée.",
                    System.Windows.MessageBoxButton.OK);
        }

        private void NotifyUserUpdateFailed()
        {
            messageBoxService.Show("Erreur",
                    "Sélectionnez d'abord un fichier Excel à partir duquel lancer la mise à jour.",
                    System.Windows.MessageBoxButton.OK);
        }

        private void ResetUpdateSource()
        {
            ExcelFilePath = string.Empty;
        }

		#region Définition des propriétés

		private string mExcelFilePath;
		public string ExcelFilePath {
			get {
				return mExcelFilePath;
			}
			set {
				mExcelFilePath = value;
				OnPropertyChanged("ExcelFilePath");
			}
		}
		
		#endregion
	}
}
