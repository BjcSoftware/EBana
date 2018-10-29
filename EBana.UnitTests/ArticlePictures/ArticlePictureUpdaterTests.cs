using System;
using NUnit.Framework;
using NSubstitute;
using OsServices.File;
using EBana.ArticlePictures;
using EBana.Models;

namespace EBana.UnitTests.ArticlePictures
{
    [TestFixture]
    class ArticlePictureUpdaterTests
    {
        [Test]
        public void UpdatePictureOfArticle_ByDefault_CopyNewPictureIntoTheRightLocation()
        {
            var stubFileService = Substitute.For<IFileService>();
            var stubFileNameFormatter = Substitute.For<IArticlePictureFileNameFormatter>();
            string updatedPictureFileName = "article.jpg";
            stubFileNameFormatter
                .Format(Arg.Any<Article>())
                .Returns(updatedPictureFileName);
            var updater = CreateArticlePictureUpdater(stubFileService, stubFileNameFormatter);

            string expectedUpdateSrc = "updateSource/newPicture.jpg";
            updater.UpdatePictureOfArticle(
                new Article(), 
                new Uri(expectedUpdateSrc, UriKind.Relative));

            string expectedUpdateDest = $"{GetPictureFolderPath()}/{updatedPictureFileName}";
            stubFileService.Received().Copy(expectedUpdateSrc, expectedUpdateDest);
        }

        private ArticlePictureUpdater CreateArticlePictureUpdater(
            IFileService fileService, 
            IArticlePictureFileNameFormatter fileNameFormatter)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var updater = new ArticlePictureUpdater(fileService, fileNameFormatter, pictureSettings);

            return updater;
        }

        private ArticlePictureSettings CreatePictureSettings()
        {
            return new ArticlePictureSettings("pictureFolder", "jpg", "default");
        }

        private string GetPictureFolderPath()
        {
            return CreatePictureSettings().PictureFolderPath;
        }
    }
}
