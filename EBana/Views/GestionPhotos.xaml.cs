using Data.Repository;
using EBana.ArticlePictures;
using EBana.Models;
using EBana.ViewModels;
using OsServices.Dialog;
using OsServices.File;
using System.Data.Entity;
using System.Windows.Controls;

namespace EBana.Views
{
	public partial class GestionPhotos : Page
	{
		public GestionPhotos()
		{
			InitializeComponent();
            DataContext = CreateViewModel();
		}

        private GestionPhotosViewModel CreateViewModel()
        {
            IFileService fileService = new WindowsFileService();
            IFileDialogService fileDialogService = new WindowsFileDialogService
            {
                Filter = "Photos (*.jpg)|*jpg|Tous les fichiers (*.*)|*.*"
            };
            IMessageBoxService messageBoxService = new MessageBoxService();

            DbContext context = new EBanaContext();
            IReader<Article> articleReader = new EfReader<Article>(context);

            var articlePictureSettings = new ArticlePictureSettings("images/photos_articles", "JPG", "default");
            var ArticlePictureFileNameFormatter = new ArticlePictureFileNameFormatter(articlePictureSettings);
            IArticlePictureLocator articlePictureLocator = new ArticlePictureLocator(fileService, ArticlePictureFileNameFormatter, articlePictureSettings);
            IArticlePictureUpdater articlePictureUpdater = new ArticlePictureUpdater(fileService, ArticlePictureFileNameFormatter, articlePictureSettings);

            return new GestionPhotosViewModel(
                articlePictureLocator, 
                articlePictureUpdater, 
                fileDialogService, 
                messageBoxService, 
                articleReader);
        }
	}
}