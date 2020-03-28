using System;
using NUnit.Framework;
using NSubstitute;
using EBana.Domain.Models;
using EBana.Services.File;
using EBana.Domain;
using EBana.Domain.ArticlePictures.Events;

namespace EBana.DesktopAppServices.ArticlePictures.UnitTests
{
    [TestFixture]
    class ArticlePictureUpdaterTests
    {
        [Test]
        public void Constructor_NullFileServicePassed_Throws()
        {
            IFileService nullService = null;
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();
            var stubHandler = Substitute.For<IEventHandler<ArticlePictureUpdated>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    nullService, 
                    stubFormatter,
                    stubHandler));
        }

        [Test]
        public void Constructor_NullFormatterPassed_Throws()
        {
            IFileService stubService = Substitute.For<IFileService>();
            IArticlePicturePathFormatter nullFormater = null;
            var stubHandler = Substitute.For<IEventHandler<ArticlePictureUpdated>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    stubService,
                    nullFormater,
                    stubHandler));
        }

        [Test]
        public void Contructor_NullHandlerPassed_Throws()
        {
            IFileService stubService = Substitute.For<IFileService>();
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();
            IEventHandler<ArticlePictureUpdated> nullHandler = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    stubService,
                    stubFormatter,
                    nullHandler));
        }

        [Test]
        public void UpdatePictureOfArticle_NullArticlePassed_Throws()
        {
            var updater = CreateUpdater();
            Article nullArticle = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => updater.UpdatePictureOfArticle(nullArticle, "aLocation"));
        }

        [Test]
        public void UpdatePictureOfArticle_NullLocationPassed_Throws()
        {
            var updater = CreateUpdater();
            string nullLocation = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => updater.UpdatePictureOfArticle(CreateStubArticle(), nullLocation));
        }

        [Test]
        public void UpdatePictureOfArticle_ByDefault_CopyNewPictureIntoTheRightLocation()
        {
            var stubFileService = Substitute.For<IFileService>();

            string updateDest = "destFolder/article.jpg";
            var stubFilePathFormater = Substitute.For<IArticlePicturePathFormatter>();
            stubFilePathFormater
                .FormatPath(Arg.Any<Article>())
                .Returns(updateDest);
            var updater = CreateUpdater(stubFileService, stubFilePathFormater);

            string updateSrc = "srcFolder/picture.jpg";
            updater.UpdatePictureOfArticle(
                CreateStubArticle(),
                updateSrc);

            stubFileService
                .Received()
                .Copy(updateSrc, updateDest);
        }

        [Test]
        public void UpdatePictureOfArticle_Always_FiresEvent()
        {
            var stubEventHandler = Substitute.For<IEventHandler<ArticlePictureUpdated>>();
            var updater = CreateUpdater(stubEventHandler);
            var articleToUpdate = CreateStubArticle();

            updater.UpdatePictureOfArticle(articleToUpdate, "newPicturePath");

            stubEventHandler.Received().Handle(
                Arg.Any<ArticlePictureUpdated>());
        }

        private ArticlePictureUpdater CreateUpdater()
        {
            var stubService = Substitute.For<IFileService>();
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();

            return CreateUpdater(stubService, stubFormatter);
        }

        private ArticlePictureUpdater CreateUpdater(
            IFileService fileService,
            IArticlePicturePathFormatter pathFormatter)
        {
            var handler = Substitute.For<IEventHandler<ArticlePictureUpdated>>();
            return CreateUpdater(
                fileService,
                pathFormatter,
                handler);
        }

        private ArticlePictureUpdater CreateUpdater(
            IFileService fileService,
            IArticlePicturePathFormatter pathFormater,
            IEventHandler<ArticlePictureUpdated> handler)
        {
            return new ArticlePictureUpdater(
                fileService,
                pathFormater,
                handler);
        }

        private ArticlePictureUpdater CreateUpdater(
            IEventHandler<ArticlePictureUpdated> handler)
        {
            var stubService = Substitute.For<IFileService>();
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();

            return new ArticlePictureUpdater(
                stubService,
                stubFormatter,
                handler);
        }

        private Article CreateStubArticle()
        {
            return
                new Article(
                    new ReferenceArticle("N1111111"),
                    "Article",
                    "Loc",
                    45,
                    "Infos");
        }
    }
}
