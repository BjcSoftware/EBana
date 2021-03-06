﻿using EBana.Domain.Models;
using EBana.EfDataAccess.Repository;
using NUnit.Framework;
using System;
using NSubstitute;
using EBana.Domain.SearchEngine;

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
            IReader<Epi> stubEpiReader = Substitute.For<IReader<Epi>>();
            IReader<Sel> stubSelReader = Substitute.For<IReader<Sel>>();

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
            IReader<Epi> stubEpiReader = Substitute.For<IReader<Epi>>();
            IReader<Sel> stubSelReader = Substitute.For<IReader<Sel>>();

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
            IReader<Epi> nullEpiReader = null;
            IReader<Sel> stubSelReader = Substitute.For<IReader<Sel>>();

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
            IReader<Epi> stubEpiReader = Substitute.For<IReader<Epi>>();
            IReader<Sel> nullSelReader = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleSearchEngine(
                    stubArticleReader,
                    stubBanaliseReader,
                    stubEpiReader,
                    nullSelReader));
        }

        [Test]
        public void PerformSearch_NullSettingsPassed_Throws()
        {
            var searchEngine = CreateSearchEngine();
            SearchSettings nullSettings = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => searchEngine.PerformSearch(nullSettings));
        }

        private ArticleSearchEngine CreateSearchEngine()
        {
            var stubArticleReader = Substitute.For<IReader<Article>>();
            var stubBanaliseReader = Substitute.For<IReader<Banalise>>();
            var stubEpiReader = Substitute.For<IReader<Epi>>();
            var stubSelReader = Substitute.For<IReader<Sel>>();

            return new ArticleSearchEngine(
                stubArticleReader,
                stubBanaliseReader,
                stubEpiReader,
                stubSelReader);
        }
    }
}
