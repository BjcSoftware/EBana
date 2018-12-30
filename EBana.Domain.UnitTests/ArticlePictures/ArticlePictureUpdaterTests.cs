using System;
using NUnit.Framework;
using NSubstitute;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Domain.ArticlePictures.UnitTests
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
