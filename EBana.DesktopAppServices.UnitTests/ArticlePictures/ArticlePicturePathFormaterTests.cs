﻿using NUnit.Framework;
using NSubstitute;
using System;
using EBana.Domain.Models;

namespace EBana.Services.DesktopAppServices.ArticlePictures.UnitTests
{
    [TestFixture]
    public class ArticlePicturePathFormaterTests
    {
        [Test]
        public void Constructor_NullSettingsPassed_Throws()
        {
            ArticlePictureSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePicturePathFormatter(
                    nullSettings,
                    Substitute.For<IArticlePictureNameFormatter>()));
        }

        [Test]
        public void Constructor_NullNameFormaterPassed_Throws()
        {
            IArticlePictureNameFormatter nullFormater = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticlePicturePathFormatter(
                    new ArticlePictureSettings("", "", ""),
                    nullFormater));
        }

        [Test]
        public void FormatPath_NullArticlePassed_Throws()
        {
            Article nullArticle = null;
            ArticlePicturePathFormatter formater = CreateFormater();

            var exception = Assert.Catch<ArgumentNullException>(
                () => formater.FormatPath(nullArticle));
        }

        [Test]
        public void FormatPath_CorrectArticlePassed_ReturnsCorrectPath()
        {
            var stubNameFormater = Substitute.For<IArticlePictureNameFormatter>();
            stubNameFormater
                .FormatName(Arg.Any<Article>())
                .Returns("ArticleName");

            var formater = new ArticlePicturePathFormatter(
                new ArticlePictureSettings("folder", "png", "default"),
                stubNameFormater);

            string expectedPath = "folder/ArticleName.png";
            string actualPath = formater.FormatPath(new Article());
            Assert.AreEqual(
                expectedPath, 
                actualPath);
        }

        private ArticlePicturePathFormatter CreateFormater()
        {
            return new ArticlePicturePathFormatter(
                new ArticlePictureSettings("", "", ""),
                Substitute.For<IArticlePictureNameFormatter>());
        }
    }
}
