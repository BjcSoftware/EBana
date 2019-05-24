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
            var stubFileNameFormater = Substitute.For<IArticlePictureNameFormater>();
            stubFileNameFormater
                .Format(Arg.Any<Article>())
                .Returns("article");
            var updater = CreateArticlePictureUpdater(stubFileService, stubFileNameFormater);

            string expectedUpdateSrc = "updateSource/newPicture.jpg";
            updater.UpdatePictureOfArticle(
                new Article(),
                new Uri(expectedUpdateSrc, UriKind.Relative));

            string expectedUpdateDest = $"{PictureFolderPath}/article.jpg";

            stubFileService.Received().Copy(expectedUpdateSrc, expectedUpdateDest);
        }

        private ArticlePictureUpdater CreateArticlePictureUpdater(
            IFileService fileService, 
            IArticlePictureNameFormater fileNameFormatter)
        {
            ArticlePictureSettings pictureSettings = CreatePictureSettings();
            var updater = new ArticlePictureUpdater(fileService, fileNameFormatter, pictureSettings);

            return updater;
        }

        private ArticlePictureSettings CreatePictureSettings()
        {
            return new ArticlePictureSettings("pictureFolder", "jpg", "default");
        }

        private string PictureFolderPath => CreatePictureSettings().PictureFolderPath;
    }
}
