using Data.Repository;
using EBana.ArticlePictures;
using EBana.Models;
using EBana.ViewModels;
using OsServices.Dialog;
using OsServices.File;
using OsServices.Web;
using System.Data.Entity;
using System.Windows.Controls;

namespace EBana.Views
{
	public partial class ConsultationCatalogue : Page
	{
		public ConsultationCatalogue()
		{
			InitializeComponent();
            DataContext = CreateViewModel();
		}

        private ConsultationCatalogueViewModel CreateViewModel()
        {
            IArticlePictureLocator articlePictureLocator = CreateArticlePictureLocator();

            IMessageBoxService messageBoxService = new MessageBoxService();
            IWebBrowserService webBrowserService = new WebBrowserService();

            DbContext context = new EBanaContext();
            IReader<TypeArticle> typeArticleReader = new EfReader<TypeArticle>(context);
            IReader<TypeEpi> typeEpiReader = new EfReader<TypeEpi>(context);

            var articleSearchEngine = CreateArticleSearchEngine();

            return new ConsultationCatalogueViewModel(
                articlePictureLocator,
                messageBoxService, 
                webBrowserService,
                typeArticleReader, 
                typeEpiReader, 
                articleSearchEngine);
        }

        private IArticlePictureLocator CreateArticlePictureLocator()
        {
            IFileService fileService = new WindowsFileService();
            var articlePictureSettings = new ArticlePictureSettings("images/photos_articles", "JPG", "default");
            IArticlePictureFileNameFormatter ArticlePictureFileNameFormatter = new ArticlePictureFileNameFormatter(articlePictureSettings);
            IArticlePictureLocator articlePictureLocator = new ArticlePictureLocator(
                fileService,
                ArticlePictureFileNameFormatter, 
                articlePictureSettings);

            return articlePictureLocator;
        }

        private ArticleSearchEngine CreateArticleSearchEngine()
        {
            DbContext context = new EBanaContext();
            IReader<Banalise> banaliseReader = new EfReader<Banalise>(context);
            IReader<EPI> epiReader = new EfReader<EPI>(context);
            IReader<SEL> selReader = new EfReader<SEL>(context);
            return new ArticleSearchEngine(banaliseReader, epiReader, selReader);
        }
    }
}