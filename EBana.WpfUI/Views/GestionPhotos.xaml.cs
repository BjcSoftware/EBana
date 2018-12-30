using EBana.Domain.ArticlePictures;
using EBana.EfDataAccess;
using EBana.EfDataAccess.Repository;
using EBana.Domain.Models;
using EBana.Services.Dialog;
using EBana.Services.File;
using EBana.WindowsServices.Dialog;
using EBana.WindowsServices.File;
using EBana.WpfUI.ViewModels;
using System.Data.Entity;
using System.Windows.Controls;
using EBana.Security.Hash;

namespace EBana.WpfUI.Views
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
            DbContext context = CreateDbContext();

            return new GestionPhotosViewModel(
                CreateArticlePictureLocator(),
                CreateArticlePictureUpdater(),
                CreateFileDialogService(),
                new MessageBoxDialogService(),
                CreateArticleSearchEngine());
        }

        private DbContext CreateDbContext()
        {
            return new EBanaContext(
                new BCryptHash());
        }

        private IArticlePictureLocator CreateArticlePictureLocator()
        {
            return new ArticlePictureLocator(
                    CreateFileService(),
                    CreateArticlePictureFileNameFormatter(),
                    CreateArticlePictureSettings());
        }

        private IArticlePictureUpdater CreateArticlePictureUpdater()
        {
            return new ArticlePictureUpdater(
                    CreateFileService(),
                    CreateArticlePictureFileNameFormatter(),
                    CreateArticlePictureSettings());
        }

        private IArticlePictureFileNameFormatter CreateArticlePictureFileNameFormatter()
        {
            return new ArticlePictureFileNameFormatter(
                CreateArticlePictureSettings());
        }

        private ArticlePictureSettings CreateArticlePictureSettings()
        {
            return new ArticlePictureSettings(
                "images/photos_articles",
                "JPG",
                "default");
        }

        private IFileService CreateFileService()
        {
            return new WindowsFileService();
        }

        private IFileDialogService CreateFileDialogService()
        {
            return new WindowsFileDialogService
            {
                Filter = "Photos (*.jpg)|*jpg|Tous les fichiers (*.*)|*.*"
            };
        }

        private ArticleSearchEngine CreateArticleSearchEngine()
        {
            DbContext context = CreateDbContext();

            return new ArticleSearchEngine(
                new EfReader<Article>(context),
                new EfReader<Banalise>(context),
                new EfReader<EPI>(context),
                new EfReader<SEL>(context));
        }
    }
}