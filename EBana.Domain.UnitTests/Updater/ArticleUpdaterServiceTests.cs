﻿using NUnit.Framework;
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
        public void Execute_NullCommand_Throws()
        {
            var service = CreateService();
            UpdateArticles nullCommand = null;

            Assert.Catch<ArgumentNullException>(() =>
                service.Execute(nullCommand));
        }

        [Test]
        public void Execute_InvalidUpdateSource_Throws()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            stubValidator
                .IsValid(Arg.Any<string>())
                .Returns(false);
            var service = CreateService(stubValidator);

            Assert.Catch<InvalidUpdateSourceException>(() => 
                service.Execute(new UpdateArticles()));
        }

        [Test]
        public void Execute_ValidUpdateSource_PerformsUpdateWithRightArticles()
        {
            var stubValidator = Substitute.For<IUpdateSourceValidator>();
            stubValidator
                .IsValid(Arg.Any<string>())
                .Returns(true);

            var newArticles = new List<Article> { CreateStubArticle(), CreateStubEpi() };
            var stubProvider = Substitute.For<IArticleProvider>();
            stubProvider
                .GetArticlesFrom(Arg.Any<string>())
                .Returns(newArticles);

            var mockUpdater = Substitute.For<IArticleStorageUpdater>();

            var service = CreateService(stubValidator, stubProvider, mockUpdater);

            service.Execute(new UpdateArticles());

            mockUpdater
                .Received()
                .ReplaceAvailableArticlesWith(newArticles);
        }

        private Article CreateStubArticle()
        {
            return
                new Article(
                    new ReferenceArticle("N1111111"),
                    "Article",
                    "Loc",
                    45,
                    "Infos");
        }

        private Article CreateStubEpi()
        {
            return
                new Epi(
                    new ReferenceArticle("N1111111"),
                    "Article",
                    "Loc",
                    45,
                    "Infos",
                    "lien",
                    new TypeEpi("Casque"));
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
