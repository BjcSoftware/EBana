using EBana.Domain.Models;
using NUnit.Framework;
using System;

namespace EBana.DesktopAppServices.ArticlePictures.UnitTests
{
    [TestFixture]
    public class ArticlePictureNameFormaterTests
    {
        [Test]
        public void Constructor_NullSettingsPassed_Throws()
        {
            ArticlePictureSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureNameFormatter(nullSettings));
        }

        [Test]
        public void Format_NullArticlePassed_Throws()
        {
            ArticlePictureNameFormatter formater = CreateFormater();
            Article nullArticle = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => formater.FormatName(nullArticle));
        }

        [Test]
        public void Format_CorrectArticlePassed_ReturnsCorrectName()
        {
            var article = CreateStubArticleFromReference(new ReferenceArticle("N0000000"));
            var formater = CreateFormater();
            string expectedFileName = article.Reference.Value;

            string actualFileName = formater.FormatName(article);

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        private Article CreateStubArticleFromReference(ReferenceArticle reference)
        {
            return
                new Article(
                    reference,
                    "Article",
                    "Loc",
                    45,
                    "Infos");
        }

        [Test]
        public void FormatDefault_Always_ReturnsDefaultName()
        {
            var settings = CreateSettings("default");
            var formater = new ArticlePictureNameFormatter(settings);
            string expectedFileName = "default";

            string actualFileName = formater.FormatDefaultName();

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        private ArticlePictureNameFormatter CreateFormater()
        {
            return new ArticlePictureNameFormatter(
                CreateSettings());
        }

        private ArticlePictureSettings CreateSettings()
        {
            return CreateSettings("default");
        }

        private ArticlePictureSettings CreateSettings(string defaultFileName)
        {
            return new ArticlePictureSettings(
                "folder",
                ".jpg",
                defaultFileName);
        }
    }
}
