using EBana.Domain.Models;
using NUnit.Framework;
using System;

namespace EBana.Domain.ArticlePictures.UnitTests
{
    [TestFixture]
    public class ArticlePictureFileNameFormatterTests
    {
        [Test]
        public void Constructor_NullSettingsPassed_Throws()
        {
            ArticlePictureSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureFileNameFormatter(nullSettings));
        }

        [Test]
        public void Format_NullArticlePassed_Throws()
        {
            ArticlePictureFileNameFormatter formatter = CreateFormatter();
            Article nullArticle = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => formatter.Format(nullArticle));
        }

        [Test]
        public void Format_CorrectArticlePassed_ReturnsCorrectFileName()
        {
            var article = new Article() { Ref = "N0000000" };
            var settings = CreateArticlePictureSettings();
            var formatter = new ArticlePictureFileNameFormatter(settings);
            string expectedFileName = 
                $"{article.Ref}.{settings.PictureFileExtension}";

            string actualFileName = formatter.Format(article);

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        [Test]
        public void FormatDefault_Always_ReturnsDefaultFileName()
        {
            var settings = CreateArticlePictureSettings();
            var formatter = new ArticlePictureFileNameFormatter(settings);
            string expectedFileName = 
                $"{settings.DefaultPictureName}.{settings.PictureFileExtension}";

            string actualFileName = formatter.FormatDefault();

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        private ArticlePictureFileNameFormatter CreateFormatter()
        {
            return new ArticlePictureFileNameFormatter(
                CreateArticlePictureSettings());
        }

        private ArticlePictureSettings CreateArticlePictureSettings()
        {
            return new ArticlePictureSettings(
                "folder",
                "ext",
                "default");
        }
    }
}
