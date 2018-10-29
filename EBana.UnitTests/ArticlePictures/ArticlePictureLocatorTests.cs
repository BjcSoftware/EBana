using EBana.ArticlePictures;
using EBana.Models;
using NSubstitute;
using NUnit.Framework;
using OsServices.File;
using System.Collections.Generic;

namespace EBana.UnitTests.ArticlePictures
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

            var stubFileNameFormatter = Substitute.For<IArticlePictureFileNameFormatter>();
            stubFileNameFormatter
                .Format(Arg.Any<Article>())
                .Returns("article.jpg");

            var locator = CreateArticlePictureLocator(stubFileService, stubFileNameFormatter);

            Article articleWithAPicture = new Article();
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

            Article articleWithoutPicture = new Article();
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

            var stubFileNameFormatter = Substitute.For<IArticlePictureFileNameFormatter>();
            stubFileNameFormatter
                .Format(Arg.Any<Article>())
                .Returns("article.jpg");

            var locator = CreateArticlePictureLocator(stubFileService, stubFileNameFormatter);

            Article articleWithAPicture = new Article();
            string returnedPicturePath = locator
                .GetArticlePictureLocationOrDefault(articleWithAPicture).OriginalString;

            Assert.AreEqual($"{GetPictureFolderPath()}/article.jpg", returnedPicturePath);
        }

        private ArticlePictureLocator CreateArticlePictureLocator()
        {
            var stubFileService = Substitute.For<IFileService>();
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var fileNameFormatter = new ArticlePictureFileNameFormatter(pictureSettings);
            var locator = new ArticlePictureLocator(stubFileService, fileNameFormatter, pictureSettings);

            return locator;
        }

        private ArticlePictureLocator CreateArticlePictureLocator(IFileService fileService)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var fileNameFormatter = new ArticlePictureFileNameFormatter(pictureSettings);
            var locator = new ArticlePictureLocator(fileService, fileNameFormatter, pictureSettings);

            return locator;
        }

        private ArticlePictureLocator CreateArticlePictureLocator(IFileService fileService,
            IArticlePictureFileNameFormatter fileNameFormatter)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var locator = new ArticlePictureLocator(fileService, fileNameFormatter, pictureSettings);

            return locator;
        }

        private string GetPictureFolderPath()
        {
            return CreatePictureSettings().PictureFolderPath;
        }

        private string GetDefaultPicturePath()
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            string pictureFolder = pictureSettings.PictureFolderPath;
            string defaultPicture = pictureSettings.DefaultPictureName;
            string fileExtension = pictureSettings.PictureFileExtension;

            return $"{pictureFolder}/{defaultPicture}.{fileExtension}";
        }

        private ArticlePictureSettings CreatePictureSettings()
        {
            return new ArticlePictureSettings("pictureFolder", "jpg", "default");
        }
    }
}
