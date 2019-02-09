using EBana.Domain.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    public class ExcelArticleProviderTests
    {
        [Test]
        public void Constructor_NullRawArticleProviderPassed_Throws()
        {
            IRawArticleProvider nullRawArticleProvider = null;
            var stubMapper = Substitute.For<IRawArticleToArticleMapper>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelArticleProvider(nullRawArticleProvider, stubMapper));
        }

        [Test]
        public void Constructor_NullRawArticleToArticleMapperPassed_Throws()
        {
            IRawArticleToArticleMapper nullMapper = null;
            var stubRawArticleProvider = Substitute.For<IRawArticleProvider>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelArticleProvider(stubRawArticleProvider, nullMapper));
        }

        [Test]
        public void GetArticles_NullSourcePassed_Throws()
        {
            var provider = CreateExcelArticleProvider();
            string nullSource = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => provider.GetArticlesFrom(nullSource));
        }

        [Test]
        public void GetArticles_TwoRawArticlesProvided_ReturnsTwoArticles()
        {
            var sampleArticle = new Article() { Libelle = "Sample" };
            var articleProvider = CreateExcelArticleProvider(
                CreateRawArticleProviderReturningTwoRawArticles(),
                CreateMapperReturning(sampleArticle));
            List<Article> expectedTwoArticles = new List<Article>() {
                sampleArticle,
                sampleArticle
            };

            List<Article> actualArticles = articleProvider.GetArticlesFrom("articles.xls").ToList();

            Assert.AreEqual(
                expectedTwoArticles,
                actualArticles);
        }

        [Test]
        public void GetArticles_NoRawArticlesProvided_ReturnsNoArticles()
        {
            var articleProvider = CreateExcelArticleProvider(
                CreateRawArticleProviderProvidingNoRawArticle());
            var expectedArticles = new List<Article>();

            var actualArticles = articleProvider.GetArticlesFrom("empty.xls");

            Assert.AreEqual(
                expectedArticles,
                actualArticles);
        }

        private ExcelArticleProvider CreateExcelArticleProvider()
        {
            var stubRawArticleProvider = Substitute.For<IRawArticleProvider>();
            var stubMapper = Substitute.For<IRawArticleToArticleMapper>();
            return CreateExcelArticleProvider(
                stubRawArticleProvider,
                stubMapper);
        }

        private ExcelArticleProvider CreateExcelArticleProvider(
            IRawArticleProvider rawArticleProvider, 
            IRawArticleToArticleMapper mapper)
        {
            return new ExcelArticleProvider(
                rawArticleProvider,
                mapper);
        }

        private ExcelArticleProvider CreateExcelArticleProvider(
            IRawArticleProvider rawArticleProvider)
        {
            var stubMapper = Substitute.For<IRawArticleToArticleMapper>();
            return CreateExcelArticleProvider(
                rawArticleProvider,
                stubMapper);
        }

        private IRawArticleProvider CreateRawArticleProviderReturningTwoRawArticles()
        {
            var rawArticleProvider = Substitute.For<IRawArticleProvider>();
            var twoRawArticles = new List<RawArticle> {
                new RawArticle(),
                new RawArticle()
            };

            rawArticleProvider
                .GetRawArticlesFrom(Arg.Any<string>())
                .Returns(twoRawArticles);

            return rawArticleProvider;
        }

        private IRawArticleToArticleMapper CreateMapperReturning(Article article)
        {
            var stubMapper = Substitute.For<IRawArticleToArticleMapper>();
            stubMapper
                .Map(Arg.Any<RawArticle>())
                .Returns(article);

            return stubMapper;
        }

        private IRawArticleProvider CreateRawArticleProviderProvidingNoRawArticle()
        {
            var emptyList = new List<RawArticle>();
            var provider = Substitute.For<IRawArticleProvider>();
            provider
                .GetRawArticlesFrom(Arg.Any<string>())
                .Returns(emptyList);

            return provider;
        }
    }
}
