using EBana.Domain.ArticlePictures;
using EBana.EfDataAccess;
using EBana.EfDataAccess.Repository;
using EBana.Domain.Models;
using EBana.Services.File;
using EBana.WindowsServices.Dialog;
using EBana.WindowsServices.File;
using EBana.WindowsServices.Web;
using EBana.WpfUI.ViewModels;
using System.Data.Entity;
using System.Windows.Controls;
using EBana.Security.Hash;

namespace EBana.WpfUI.Views
{
	public partial class Catalogue : Page
	{
		public Catalogue()
		{
			InitializeComponent();
            DataContext = CreateViewModel();
		}

        private CatalogueViewModel CreateViewModel()
        {
            DbContext context = CreateDbContext();

            return new CatalogueViewModel(
                CreateArticlePictureLocator(),
                new MessageBoxDialogService(),
                new WebBrowserService(),
                new SearchCriteriaProvider(
                    new EfReader<TypeArticle>(context),
                    new EfReader<TypeEpi>(context)),
                CreateArticleSearchEngine());
        }

        private IArticlePictureLocator CreateArticlePictureLocator()
        {
            IFileService fileService = new WindowsFileService();

            var articlePictureSettings = new ArticlePictureSettings(
                "images/photos_articles", 
                "JPG", 
                "default");

            IArticlePictureFileNameFormatter articlePictureFileNameFormatter = 
                new ArticlePictureFileNameFormatter(articlePictureSettings);

            IArticlePictureLocator articlePictureLocator = new ArticlePictureLocator(
                fileService,
                articlePictureFileNameFormatter, 
                articlePictureSettings);

            return articlePictureLocator;
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

        private DbContext CreateDbContext()
        {
            return new EBanaContext(
                new BCryptHash());
        }
    }
}