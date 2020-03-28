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

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ArticleRepository(
                    nullArticleWriter));
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

            return new ArticleRepository(
                stubArticleWriter);
        }
    }
}
