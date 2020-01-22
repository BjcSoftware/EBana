using EBana.DesktopAppServices.ArticlePictures;
using EBana.Domain;
using EBana.Domain.ArticlePictures;
using EBana.Domain.Models;
using EBana.Domain.Security;
using EBana.EfDataAccess;
using EBana.EfDataAccess.Repository;
using EBana.Excel;
using EBana.PresentationLogic.Core;
using EBana.PresentationLogic.ViewModels;
using EBana.Security.Hash;
using EBana.Services.Dialog;
using EBana.Services.File;
using EBana.WindowsServices.Dialog;
using EBana.WindowsServices.File;
using EBana.WpfUI.Views;
using System;
using System.Data.Entity;
using System.Windows.Controls;
using EBana.WindowsService;
using EBana.Domain.SearchEngine;
using EBana.DesktopAppServices.ArticlePictures.EventHandlers;
using EBana.DesktopAppServices.Security.EventHandlers;
using EBana.Domain.ArticleStorageUpdater;
using EBana.DesktopAppServices.ArticleStorageUpdater.EventHandlers;
using EBana.Excel.Core;
using EBana.Domain.Updater;
using EBana.Domain.Commands;

namespace EBana.WpfUI.Core
{
    public class PageComposer
    {
        // Dépendances avec un cycle de vie de Singleton
        private readonly INavigationService navigator;
        private readonly IFileService fileService;
        private readonly ArticlePictureSettings articlePictureSettings;
        private readonly IArticlePictureNameFormatter articlePictureNameFormatter;
        private readonly IArticlePictureLocator pictureLocator;
        private readonly IHash hash;
        private readonly IMessageBoxDialogService messageBoxDialogService;

        public PageComposer(INavigationService navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException(nameof(navigator));

            // initialisation des dépendances avec un cycle de vie de singleton
            this.navigator = navigator;

            fileService = new WindowsFileService();

            articlePictureSettings = CreateArticlePictureSettings();

            articlePictureNameFormatter = new ArticlePictureNameFormatter(
                articlePictureSettings);

            pictureLocator = new ArticlePictureLocator(
                fileService,
                articlePictureNameFormatter,
                articlePictureSettings);

            hash = new BCryptHash();

            messageBoxDialogService = new MessageBoxDialogService();

            CreatePictureFolderIfDoesNotExist();
        }

        private void CreatePictureFolderIfDoesNotExist()
        {
            if (!fileService.DirectoryExists(articlePictureSettings.PictureFolderPath))
            {
                fileService.CreateDirectory(articlePictureSettings.PictureFolderPath);
            }
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
                pictureLocator,
                new WindowsWebBrowserService(),
                new SearchCriteriaProvider(
                    new EfReader<TypeArticle>(context),
                    new EfReader<TypeEpi>(context)),
                CreateArticleSearchEngine());
        }

        private ArticlePictureSettings CreateArticlePictureSettings()
        {
            return new ArticlePictureSettings(
                "images/photos_articles",
                "JPG",
                "default");
        }

        private IArticleSearchEngine CreateArticleSearchEngine()
        {
            DbContext context = CreateDbContext();

            return new SearchEngineNoResultFoundNotifier(
                new ArticleSearchEngine(
                    new EfReader<Article>(context),
                    new EfReader<Banalise>(context),
                    new EfReader<EPI>(context),
                    new EfReader<SEL>(context)),
                new MessageBoxDialogService()
            );
        }

        private DbContext CreateDbContext()
        {
            return new EBanaContext(hash);
        }

        private MaintenanceConnexionViewModel CreateMaintenanceLoginViewModel()
        {
            return new MaintenanceConnexionViewModel(
                messageBoxDialogService,
                CreateAuthenticator(),
                navigator);
        }

        private IAuthenticator CreateAuthenticator()
        {
            return new Authenticator(
                CreateCredentialsReader(),
                hash);
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
                CreateUpdater(),
                CreateExcelFileDialogService());
        }

        private ICommandService<UpdateArticles> CreateUpdater()
        {
            return
                new InvalidUpdateSourceUserNotifier(
                    new ArticleUpdaterService(
                        new SimpleUpdateSourceValidator(),
                        CreateArticleProvider(),
                        CreateStorageUpdater()
                    ),
                    messageBoxDialogService
                );
        }

        private IFileDialogService CreateExcelFileDialogService()
        {
            return new WindowsFileDialogService
            {
                Filter = "Fichiers Excel (*.xlsx;*.xls)|*xlsx;*xls|Tous les fichiers (*.*)|*.*"
            };
        }

        private IArticleStorageUpdater CreateStorageUpdater()
        {
            DbContext context = CreateDbContext();

            IWriter<Article> articleWriter =
                new EfWriter<Article>(context);
            IWriter<TypeEpi> typeEpiWriter =
                new EfWriter<TypeEpi>(context);

            return new ArticleStorageUpdater(
                new ArticleRepository(
                    articleWriter,
                    typeEpiWriter),
                new ArticleStorageUpdatedUserNotifier(
                    messageBoxDialogService));
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
                pictureLocator,
                CreateArticlePictureUpdater(),
                CreatePictureFileDialogService(),
                CreateArticleSearchEngine());
        }

        private IArticlePictureUpdater CreateArticlePictureUpdater()
        {
            return new ArticlePictureUpdater(
                fileService,
                CreateArticlePictureFilePathFormatter(),
                new ArticlePictureUpdatedUserNotifier(
                    messageBoxDialogService));
        }

        private IFileDialogService CreatePictureFileDialogService()
        {
            return new WindowsFileDialogService
            {
                Filter = "Photos (*.jpg)|*jpg|Tous les fichiers (*.*)|*.*"
            };
        }

        private IArticlePicturePathFormatter CreateArticlePictureFilePathFormatter()
        {
            return new ArticlePicturePathFormatter(
                articlePictureSettings,
                articlePictureNameFormatter);
        }

        private NouveauMotDePasseViewModel CreatePasswordUpdaterViewModel()
        {
            return new NouveauMotDePasseViewModel(
                messageBoxDialogService,
                CreateAuthenticator(),
                CreatePasswordUpdater());
        }

        private IPasswordUpdater CreatePasswordUpdater()
        {
            return new PasswordUpdater(
                CreateCredentialsUpdater(),
                hash,
                new PasswordUpdatedUserNotifier(
                    messageBoxDialogService));
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
