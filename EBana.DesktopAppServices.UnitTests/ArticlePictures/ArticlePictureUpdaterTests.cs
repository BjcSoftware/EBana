using System;
using NUnit.Framework;
using NSubstitute;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Services.DesktopAppServices.ArticlePictures.UnitTests
{
    [TestFixture]
    class ArticlePictureUpdaterTests
    {
        [Test]
        public void Constructor_NullFileServicePassed_Throws()
        {
            IFileService nullService = null;
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();
            var stubSettings = new ArticlePictureSettings("", "", "");

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    nullService, 
                    stubFormatter, 
                    stubSettings));
        }

        [Test]
        public void Constructor_NullFormatterPassed_Throws()
        {
            IFileService stubService = Substitute.For<IFileService>();
            IArticlePicturePathFormatter nullFormater = null;
            var stubSettings = new ArticlePictureSettings("", "", "");

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    stubService,
                    nullFormater,
                    stubSettings));
        }

        [Test]
        public void Contructor_NullSettingsPassed_Throws()
        {
            IFileService stubService = Substitute.For<IFileService>();
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();
            ArticlePictureSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureUpdater(
                    stubService,
                    stubFormatter,
                    nullSettings));
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
                () => updater.UpdatePictureOfArticle(new Article(), nullLocation));
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
                new Article(),
                updateSrc);

            stubFileService
                .Received()
                .Copy(updateSrc, updateDest);
        }

        private ArticlePictureUpdater CreateUpdater()
        {
            var stubService = Substitute.For<IFileService>();
            var stubFormatter = Substitute.For<IArticlePicturePathFormatter>();
            var stubSettings = new ArticlePictureSettings("", "", "");

            return new ArticlePictureUpdater(
                stubService,
                stubFormatter,
                stubSettings);
        }

        private ArticlePictureUpdater CreateUpdater(
            IFileService fileService, 
            IArticlePicturePathFormatter pathFormater)
        {
            var settings = new ArticlePictureSettings("pictureFolder", "jpg", "default");
            return new ArticlePictureUpdater(
                fileService, 
                pathFormater, 
                settings);
        }
    }
}
