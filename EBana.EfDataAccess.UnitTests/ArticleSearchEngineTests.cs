using EBana.Domain.Models;
using EBana.EfDataAccess.Repository;
using NUnit.Framework;
using System;
using NSubstitute;

namespace EBana.EfDataAccess.UnitTests
{
    [TestFixture]
    public class ArticleSearchEngineTests
    {
        [Test]
        public void Constructor_NullArticleReaderPassed_Throws()
        {
            IReader<Article> nullArticleReader = null;
            IReader<Banalise> nullBanaliseReader = Substitute.For<IReader<Banalise>>();
            IReader<EPI> stubEpiReader = Substitute.For<IReader<EPI>>();
            IReader<SEL> stubSelReader = Substitute.For<IReader<SEL>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleSearchEngine(
                    nullArticleReader,
                    nullBanaliseReader,
                    stubEpiReader,
                    stubSelReader));
        }

        [Test]
        public void Constructor_NullBanaliseReaderPassed_Throws()
        {
            IReader<Article> stubArticleReader = Substitute.For<IReader<Article>>();
            IReader<Banalise> nullBanaliseReader = null;
            IReader<EPI> stubEpiReader = Substitute.For<IReader<EPI>>();
            IReader<SEL> stubSelReader = Substitute.For<IReader<SEL>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleSearchEngine(
                    stubArticleReader,
                    nullBanaliseReader,
                    stubEpiReader,
                    stubSelReader));
        }

        [Test]
        public void Constructor_NullEpiReaderPassed_Throws()
        {
            IReader<Article> stubArticleReader = Substitute.For<IReader<Article>>();
            IReader<Banalise> stubBanaliseReader = Substitute.For<IReader<Banalise>>();
            IReader<EPI> nullEpiReader = null;
            IReader<SEL> stubSelReader = Substitute.For<IReader<SEL>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleSearchEngine(
                    stubArticleReader,
                    stubBanaliseReader,
                    nullEpiReader,
                    stubSelReader));
        }

        [Test]
        public void Constructor_NullSelReaderPassed_Throws()
        {
            IReader<Article> stubArticleReader = Substitute.For<IReader<Article>>();
            IReader<Banalise> stubBanaliseReader = Substitute.For<IReader<Banalise>>();
            IReader<EPI> stubEpiReader = Substitute.For<IReader<EPI>>();
            IReader<SEL> nullSelReader = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleSearchEngine(
                    stubArticleReader,
                    stubBanaliseReader,
                    stubEpiReader,
                    nullSelReader));
        }

        [Test]
        public void SearchBanalise_NullSearchQueryPassed_Throws()
        {
            ArticleSearchEngine searchEngine = CreateSearchEngine();
            string nullSearchQuery = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => searchEngine.SearchBanalise(nullSearchQuery));
        }

        [Test]
        public void SearchEpi_NullSearchQueryPassed_Throws()
        {
            ArticleSearchEngine searchEngine = CreateSearchEngine();
            string nullSearchQuery = null;
            TypeEpi typeEpi = new TypeEpi();

            var exception = Assert.Throws<ArgumentNullException>(
                () => searchEngine.SearchEpi(
                    nullSearchQuery,
                    typeEpi));
        }

        [Test]
        public void SearchEpi_NullTypeEpiPassed_Throws()
        {
            ArticleSearchEngine searchEngine = CreateSearchEngine();
            string searchQuery = "query";
            TypeEpi nullTypeEpi = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => searchEngine.SearchEpi(
                    searchQuery,
                    nullTypeEpi));
        }

        [Test]
        public void SearchSel_NullSearchQueryPassed_Throws()
        {
            ArticleSearchEngine searchEngine = CreateSearchEngine();
            string nullSearchQuery = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => searchEngine.SearchSel(nullSearchQuery));
        }

        private ArticleSearchEngine CreateSearchEngine()
        {
            var stubArticleReader = Substitute.For<IReader<Article>>();
            var stubBanaliseReader = Substitute.For<IReader<Banalise>>();
            var stubEpiReader = Substitute.For<IReader<EPI>>();
            var stubSelReader = Substitute.For<IReader<SEL>>();

            return new ArticleSearchEngine(
                stubArticleReader,
                stubBanaliseReader,
                stubEpiReader,
                stubSelReader);
        }
    }
}
