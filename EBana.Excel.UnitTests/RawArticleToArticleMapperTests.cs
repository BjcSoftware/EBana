using EBana.Domain;
using EBana.Domain.Models;
using NUnit.Framework;
using System;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    class RawArticleToArticleMapperTests
    {
        [Test]
        public void Constructor_NullSettingsPassed_Throws()
        {
            IArticleSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new RawArticleToArticleMapper(nullSettings));
        }

        [Test]
        public void Map_NullRawArticlePassed_Throws()
        {
            RawArticleToArticleMapper mapper = CreateMapper();
            RawArticle nullRawArticle = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => mapper.Map(nullRawArticle));
        }

        [Test]
        public void Map_ValidRawArticleBanalisePassed_ReturnsBanalise()
        {
            RawArticleToArticleMapper mapper = CreateMapper();
            RawArticle rawArticle = CreateValidRawArticleBanalise();

            Article article = mapper.Map(rawArticle);

            Assert.IsInstanceOf<Banalise>(article);
        }

        private RawArticle CreateValidRawArticleBanalise()
        {
            var validRawArticleBanalise = new RawArticle()
            {
                Ref = "N0000000",
                Libelle = "Libelle",
                Localisation = "Localisation",
                IdMagasin = articlesSettings.GetIdMagasinBanalise()
            };

            return validRawArticleBanalise;
        }

        [Test]
        public void Map_ValidRawArticleEpiPassed_ReturnsEPI()
        {
            RawArticleToArticleMapper mapper = CreateMapper();
            RawArticle rawArticle = CreateValidRawArticleEPI();

            Article article = mapper.Map(rawArticle);

            Assert.IsInstanceOf<EPI>(article);
        }

        private RawArticle CreateValidRawArticleEPI()
        {
            var validRawArticleEPI = new RawArticle()
            {
                Ref = "N0000000",
                Libelle = "Libelle",
                Localisation = "Localisation",
                IdMagasin = articlesSettings.GetIdMagasinBanalise(),
                TypeEpi = new TypeEpi() { Libelle = "TypeEPI" }
            };

            return validRawArticleEPI;
        }

        [Test]
        public void Map_ValidRawArticleSelPassed_ReturnsSEL()
        {
            RawArticleToArticleMapper mapper = CreateMapper();
            RawArticle rawArticle = CreateValidRawArticleSEL();

            Article article = mapper.Map(rawArticle);

            Assert.IsInstanceOf<SEL>(article);
        }

        private RawArticle CreateValidRawArticleSEL()
        {
            var validRawArticleSEL = new RawArticle()
            {
                Ref = "I0000000",
                Libelle = "Libelle",
                Localisation = "Localisation",
                IdMagasin = articlesSettings.GetIdMagasinSEL()
            };

            return validRawArticleSEL;
        }

        private RawArticleToArticleMapper CreateMapper()
        {
            return new RawArticleToArticleMapper(articlesSettings);
        }

        private ArticleSettings articlesSettings = new ArticleSettings();
    }
}
