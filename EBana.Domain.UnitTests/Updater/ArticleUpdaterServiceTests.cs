using NUnit.Framework;
using NSubstitute;
using System;
using EBana.Domain.ArticleStorageUpdater;
using EBana.Domain.Updater.Exceptions;
using System.Collections.Generic;
using EBana.Domain.Models;

namespace EBana.Domain.Updater.UnitTests
{
    [TestFixture]
    public class ArticleUpdaterServiceTests
    {
        [Test]
        public void Constructor_NullValidatorPassed_Throws()
        {
            IUpdateSourceValidator nullValidator = null;
            var stubProvider = Substitute.For<IArticleProvider>();
            var stubUpdater = Substitute.For<IArticleStorageUpdater>();

            Assert.Catch<ArgumentNullException>(() =>
                new ArticleUpdaterService(
                    nullValidator,
                    stubProvider,
                    stubUpdater));
        }

        [Test]
        public void Constructor_NullProviderPassed_Throws()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            IArticleProvider nullProvider = null;
            var stubUpdater = Substitute.For<IArticleStorageUpdater>();

            Assert.Catch<ArgumentNullException>(() =>
                new ArticleUpdaterService(
                    stubValidator,
                    nullProvider,
                    stubUpdater));
        }

        [Test]
        public void Constructor_NullUpdaterPassed_Throws()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            var stubProvider = Substitute.For<IArticleProvider>();
            IArticleStorageUpdater nullUpdater = null;

            Assert.Catch<ArgumentNullException>(() =>
                new ArticleUpdaterService(
                    stubValidator,
                    stubProvider,
                    nullUpdater));
        }

        [Test]
        public void UpdateArticles_NullSource_Throws()
        {
            var service = CreateService();
            string nullSource = null;

            Assert.Catch<ArgumentNullException>(() =>
                service.UpdateArticles(nullSource));
        }

        [Test]
        public void UpdateArticles_InvalidSource_Throws()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            stubValidator
                .IsValid(Arg.Any<string>())
                .Returns(false);
            var service = CreateService(stubValidator);

            Assert.Catch<InvalidUpdateSourceException>(() => 
                service.UpdateArticles("source"));
        }

        [Test]
        public void UpdateArticles_ValidSource_PerformsUpdateWithRightArticles()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            stubValidator
                .IsValid(Arg.Any<string>())
                .Returns(true);

            var newArticles = new List<Article> { new Article(), new EPI() };
            var stubProvider = Substitute.For<IArticleProvider>();
            stubProvider
                .GetArticlesFrom(Arg.Any<string>())
                .Returns(newArticles);

            var mockUpdater = Substitute.For<IArticleStorageUpdater>();

            var service = CreateService(stubValidator, stubProvider, mockUpdater);

            service.UpdateArticles("source");

            mockUpdater
                .Received()
                .ReplaceAvailableArticlesWith(newArticles);
        }

        ArticleUpdaterService CreateService()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            var stubProvider = Substitute.For<IArticleProvider>();
            var stubUpdater = Substitute.For<IArticleStorageUpdater>();

            return new ArticleUpdaterService(
                stubValidator,
                stubProvider,
                stubUpdater);
        }

        ArticleUpdaterService CreateService(
            IUpdateSourceValidator validator)
        {
            var stubUpdater = Substitute.For<IArticleStorageUpdater>();

            return CreateService(
                validator, 
                stubUpdater);
        }

        ArticleUpdaterService CreateService(
            IUpdateSourceValidator validator,
            IArticleStorageUpdater updater)
        {
            var stubProvider = Substitute.For<IArticleProvider>();

            return CreateService(
                validator,
                stubProvider,
                updater);
        }

        ArticleUpdaterService CreateService(
            IUpdateSourceValidator validator,
            IArticleProvider provider,
            IArticleStorageUpdater updater)
        {
            return new ArticleUpdaterService(
                validator,
                provider,
                updater);
        }
    }
}
