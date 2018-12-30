using EBana.Domain.Models;
using EBana.EfDataAccess.Repository;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace EBana.EfDataAccess.UnitTests
{
    [TestFixture]
    public class ArticleRepositoryTests
    {
        [Test]
        public void Constructor_NullArticleWriterPassed_Throws()
        {
            IWriter<Article> nullArticleWriter = null;
            var stubTypeEpiWriter = Substitute.For<IWriter<TypeEpi>>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleRepository(
                    nullArticleWriter,
                    stubTypeEpiWriter));
        }

        [Test]
        public void Constructor_NullTypeEpiWriterPassed_Throws()
        {
            var stubArticleWriter = Substitute.For<IWriter<Article>>();
            IWriter<TypeEpi> nullTypeEpiWriter = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleRepository(
                    stubArticleWriter,
                    nullTypeEpiWriter));
        }

        [Test]
        public void AddRange_NullArticlesPassed_Throws()
        {
            ArticleRepository repository = CreateRepository();
            IEnumerable<Article> nullArticles = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => repository.AddRange(nullArticles));
        }

        private ArticleRepository CreateRepository()
        {
            var stubArticleWriter = Substitute.For<IWriter<Article>>();
            var stubTypeEpiWriter = Substitute.For<IWriter<TypeEpi>>();

            return new ArticleRepository(
                stubArticleWriter,
                stubTypeEpiWriter);
        }
    }
}
