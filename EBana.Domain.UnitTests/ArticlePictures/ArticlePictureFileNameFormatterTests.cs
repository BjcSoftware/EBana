using EBana.Domain.Models;
using NUnit.Framework;
using System;

namespace EBana.Domain.ArticlePictures.UnitTests
{
    [TestFixture]
    public class ArticlePictureNameFormaterTests
    {
        [Test]
        public void Constructor_NullSettingsPassed_Throws()
        {
            ArticlePictureSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePictureNameFormater(nullSettings));
        }

        [Test]
        public void Format_NullArticlePassed_Throws()
        {
            ArticlePictureNameFormater formater = CreateFormater();
            Article nullArticle = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => formater.Format(nullArticle));
        }

        [Test]
        public void Format_CorrectArticlePassed_ReturnsCorrectName()
        {
            var article = new Article() { Ref = "N0000000" };
            var formater = CreateFormater();
            string expectedFileName = article.Ref;

            string actualFileName = formater.Format(article);

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        [Test]
        public void FormatDefault_Always_ReturnsDefaultName()
        {
            var settings = CreateSettings("default");
            var formater = new ArticlePictureNameFormater(settings);
            string expectedFileName = "default";

            string actualFileName = formater.FormatDefault();

            Assert.AreEqual(
                expectedFileName,
                actualFileName);
        }

        private ArticlePictureNameFormater CreateFormater()
        {
            return new ArticlePictureNameFormater(
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
