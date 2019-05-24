using EBana.Domain;
using EBana.Domain.ArticlePictures;
using EBana.Domain.Models;
using EBana.Domain.Security;
using EBana.EfDataAccess;
using EBana.EfDataAccess.Repository;
using EBana.Excel;
using EBana.Security.Hash;
using EBana.Services.Dialog;
using EBana.Services.File;
using EBana.WindowsServices.Dialog;
using EBana.WindowsServices.File;
using EBana.WindowsServices.Web;
using EBana.WpfUI.ViewModels;
using EBana.WpfUI.Views;
using System;
using System.Data.Entity;
using System.Windows.Controls;

namespace EBana.WpfUI.Core
{
    public class PageComposer
    {
        private readonly INavigationService navigator;

        public PageComposer(INavigationService navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException(nameof(navigator));

            this.navigator = navigator;
        }

        public Page CreatePage(string pageName)
        {
            if (pageName == null)
                throw new ArgumentNullException(nameof(pageName));

            switch (pageName)
            {
                case "MainMenu":
                    return new MainMenu(
                        CreateMainMenuViewModel());
                case "About":
                    return new About();
                case "Catalogue":
                    return new Catalogue(
                        CreateCatalogueViewModel());
                case "MaintenanceLogin":
                    return new MaintenanceConnexion(
                        CreateMaintenanceLoginViewModel());
                case "MaintenanceMenu":
                    return new MenuMaintenance(
                        CreateMaintenanceMenuViewModel());
                case "ArticlesUpdater":
                    return new UpdateDatabase(
                        CreateUpdaterViewModel());
                case "PictureManager":
                    return new GestionPhotos(
                        CreatePictureManagerViewModel());
                case "PasswordUpdater":
                    return new NouveauMotDePasse(
                        CreatePasswordUpdaterViewModel());
                default:
                    throw new Exception($"Invalid page: {pageName}");
            }
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(navigator);
        }

        private CatalogueViewModel CreateCatalogueViewModel()
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
            return new ArticlePictureLocator(
                CreateFileService(),
                CreateArticlePictureNameFormater(),
                CreateArticlePictureSettings());
        }

        private IArticlePictureNameFormater CreateArticlePictureNameFormater()
        {
            return new ArticlePictureNameFormater(
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
            return new EBanaContext(CreateHash());
        }

        private IHash CreateHash()
        {
            return new BCryptHash();
        }

        private MaintenanceConnexionViewModel CreateMaintenanceLoginViewModel()
        {
            return new MaintenanceConnexionViewModel(
                new MessageBoxDialogService(),
                CreateAuthenticator(),
                navigator);
        }

        private IAuthenticator CreateAuthenticator()
        {
            return new Authenticator(
                CreateCredentialsReader(),
                CreateHash());
        }

        private ICredentialsReader CreateCredentialsReader()
        {
            // il faut ici utiliser un Reader sans système de cache au cas où l'utilisateur change de mot de passe et se reconnecte sans quitter l'application entre deux.
            // Si le cache était actif, l'ancien mot de passe resterait en cache et remplacerait le nouveau tant que l'utilisateur ne redémarre pas l'application.
            IReader<Credentials> credentialsReaderWithoutCaching =
                new EfReaderWithoutCaching<Credentials>(
                    new EfReader<Credentials>(
                        CreateDbContext()));

            return new CredentialsReader(
                credentialsReaderWithoutCaching);
        }

        private MaintenanceMenuViewModel CreateMaintenanceMenuViewModel()
        {
            return new MaintenanceMenuViewModel(navigator);
        }

        private UpdateArticlesViewModel CreateUpdaterViewModel()
        {
            return new UpdateArticlesViewModel(
                CreateExcelFileDialogService(),
                new MessageBoxDialogService(),
                CreateUpdater(),
                CreateArticleProvider());
        }

        private IFileDialogService CreateExcelFileDialogService()
        {
            return new WindowsFileDialogService
            {
                Filter = "Fichiers Excel (*.xlsx;*.xls)|*xlsx;*xls|Tous les fichiers (*.*)|*.*"
            };
        }

        private IArticleStorageUpdater CreateUpdater()
        {
            DbContext context = CreateDbContext();

            IWriter<Article> articleWriter =
                new EfWriter<Article>(context);
            IWriter<TypeEpi> typeEpiWriter =
                new EfWriter<TypeEpi>(context);

            return new ArticleStorageUpdater(
                new ArticleRepository(
                    articleWriter,
                    typeEpiWriter));
        }

        private IArticleProvider CreateArticleProvider()
        {
            return new ExcelArticleProvider(
                CreateRawArticleProvider(),
                CreateRawArticleToArticleMapper());
        }

        private IRawArticleProvider CreateRawArticleProvider()
        {
            return new ExcelRawArticleProvider(
                new RecordToRawArticleMapperWithEpiCaching(
                    new RecordToRawArticleMapper(
                        new ArticleFieldToRecordFieldMapping())),
                new ExcelFileFactory());
        }

        private IRawArticleToArticleMapper CreateRawArticleToArticleMapper()
        {
            return new RawArticleToArticleMapper(
                new ArticleSettings());
        }

        private GestionPhotosViewModel CreatePictureManagerViewModel()
        {
            return new GestionPhotosViewModel(
                CreateArticlePictureLocator(),
                CreateArticlePictureUpdater(),
                CreatePictureFileDialogService(),
                new MessageBoxDialogService(),
                CreateArticleSearchEngine());
        }

        private IArticlePictureUpdater CreateArticlePictureUpdater()
        {
            return new ArticlePictureUpdater(
                CreateFileService(),
                CreateArticlePictureFileNameFormatter(),
                CreateArticlePictureSettings());
        }

        private IFileDialogService CreatePictureFileDialogService()
        {
            return new WindowsFileDialogService
            {
                Filter = "Photos (*.jpg)|*jpg|Tous les fichiers (*.*)|*.*"
            };
        }

        private IArticlePictureNameFormater CreateArticlePictureFileNameFormatter()
        {
            return new ArticlePictureNameFormater(
                CreateArticlePictureSettings());
        }

        private NouveauMotDePasseViewModel CreatePasswordUpdaterViewModel()
        {
            return new NouveauMotDePasseViewModel(
                new MessageBoxDialogService(),
                CreateAuthenticator(),
                CreatePasswordUpdater());
        }

        private IPasswordUpdater CreatePasswordUpdater()
        {
            return new PasswordUpdater(
                CreateCredentialsUpdater(),
                CreateHash());
        }

        private ICredentialsUpdater CreateCredentialsUpdater()
        {
            return new CredentialsUpdater(
                CreateCredentialsWriter(),
                CreateCredentialsReader());
        }

        private IWriter<Credentials> CreateCredentialsWriter()
        {
            return new EfWriter<Credentials>(
                CreateDbContext());
        }
    }
}
