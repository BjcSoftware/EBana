using EBana.Domain.Models;
using EBana.Services.File;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace EBana.DesktopAppServices.ArticlePictures.UnitTests
{
    [TestFixture]
    class ArticlePictureLocatorTests
    {
        [Test]
        public void IsArticleHavingAPicture_NullArticle_ReturnsFalse()
        {
            ArticlePictureLocator locator = CreateArticlePictureLocator();

            bool nullArticleHavingAPicture = locator.IsArticleHavingAPicture(null);

            Assert.IsFalse(nullArticleHavingAPicture);
        }

        [Test]
        public void IsArticleHavingAPicture_ArticleWithAPicture_ReturnsTrue()
        {
            var pictureNames = new List<string>() { "article.jpg" };
            var stubFileService = Substitute.For<IFileService>();
            stubFileService
                .GetAllFileNamesInFolder(Arg.Any<string>())
                .Returns(pictureNames);

            var stubNameFormater = Substitute.For<IArticlePictureNameFormatter>();
            stubNameFormater
                .FormatName(Arg.Any<Article>())
                .Returns("article");

            var locator = CreateArticlePictureLocator(stubFileService, stubNameFormater);

            Article articleWithAPicture = CreateStubArticle();
            bool IsArticleHavingAPicture = locator
                .IsArticleHavingAPicture(articleWithAPicture);

            Assert.IsTrue(IsArticleHavingAPicture);
        }

        [Test]
        public void GetArticlePictureLocationOrDefault_NullArticle_ReturnsDefault()
        {
            ArticlePictureLocator locator = CreateArticlePictureLocator();

            string returnedPicturePath = locator
                .GetArticlePictureLocationOrDefault(null)
                .OriginalString;

            Assert.AreEqual(GetDefaultPicturePath(), returnedPicturePath);
        }

        [Test]
        public void GetArticlePictureLocationOrDefault_ArticleWithoutPicture_ReturnsDefault()
        {
            var emptyListOfPictureNames = new List<string>();
            var stubFileService = Substitute.For<IFileService>();
            stubFileService.GetAllFileNamesInFolder(Arg.Any<string>())
                .Returns(emptyListOfPictureNames);

            var locator = CreateArticlePictureLocator(stubFileService);

            Article articleWithoutPicture = CreateStubArticle();
            string returnedPicturePath = locator
                .GetArticlePictureLocationOrDefault(articleWithoutPicture)
                .OriginalString;

            Assert.AreEqual(GetDefaultPicturePath(), returnedPicturePath);
        }

        [Test]
        public void GetArticlePictureLocationOrDefault_ArticleWithPicture_ReturnsCorrectPicture()
        {
            var pictureNames = new List<string>() { "article.jpg" };
            var stubFileService = Substitute.For<IFileService>();
            stubFileService
                .GetAllFileNamesInFolder(Arg.Any<string>())
                .Returns(pictureNames);

            var stubNameFormater = Substitute.For<IArticlePictureNameFormatter>();
            stubNameFormater
                .FormatName(Arg.Any<Article>())
                .Returns("article");

            var locator = CreateArticlePictureLocator(stubFileService, stubNameFormater);

            Article articleWithAPicture = CreateStubArticle();
            string returnedPicturePath = locator
                .GetArticlePictureLocationOrDefault(articleWithAPicture).OriginalString;

            Assert.AreEqual($"{GetPictureFolderPath()}/article.jpg", returnedPicturePath);
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

        private ArticlePictureLocator CreateArticlePictureLocator()
        {
            var stubFileService = Substitute.For<IFileService>();
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var fileNameFormater = new ArticlePictureNameFormatter(pictureSettings);
            var locator = new ArticlePictureLocator(stubFileService, fileNameFormater, pictureSettings);

            return locator;
        }

        private ArticlePictureLocator CreateArticlePictureLocator(IFileService fileService)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var fileNameFormater = new ArticlePictureNameFormatter(pictureSettings);
            var locator = new ArticlePictureLocator(fileService, fileNameFormater, pictureSettings);

            return locator;
        }

        private ArticlePictureLocator CreateArticlePictureLocator(
            IFileService fileService,
            IArticlePictureNameFormatter fileNameFormater)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var locator = new ArticlePictureLocator(fileService, fileNameFormater, pictureSettings);

            return locator;
        }

        private string GetPictureFolderPath()
        {
            return CreatePictureSettings().PictureFolderPath;
        }

        private string GetDefaultPicturePath()
        {
            var pictureSettings = CreatePictureSettings();
            return pictureSettings.FormatPicturePath("default");
        }

        private ArticlePictureSettings CreatePictureSettings()
        {
            return new ArticlePictureSettings("pictureFolder", "jpg", "default");
        }

        private ArticlePictureSettings CreatePictureSettings(string defaultFileName, string fileExtension)
        {
            return new ArticlePictureSettings("pictureFolder", fileExtension, defaultFileName);
        }
    }
}
