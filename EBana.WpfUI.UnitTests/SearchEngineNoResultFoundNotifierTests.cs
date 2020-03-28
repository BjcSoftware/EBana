using EBana.Domain.SearchEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using EBana.Services.Dialog;
using EBana.Domain.Models;

namespace EBana.WpfUI.UnitTests
{
    [TestFixture]
    public class SearchEngineNoResultFoundNotifierTests
    {
        [Test]
        public void Constructor_NullDecoratedSearchEnginePassed_Throws()
        {
            IArticleSearchEngine nullSearchEngine = null;
            var stubDialogService = Substitute.For<IMessageBoxDialogService>();

            Assert.Catch<ArgumentNullException>(() =>
                new SearchEngineNoResultFoundNotifier(
                    nullSearchEngine,
                    stubDialogService));
        }

        [Test]
        public void Constructor_NullDialogServicePassed_Throws()
        {
            var stubSearchEngine = Substitute.For<IArticleSearchEngine>();
            IMessageBoxDialogService nullDialogService = null;

            Assert.Catch<ArgumentNullException>(() =>
                new SearchEngineNoResultFoundNotifier(
                    stubSearchEngine,
                    nullDialogService));
        }

        [Test]
        public void PerformSearch_NoResultFound_NotifiesUser()
        {
            var stubSearchEngine = Substitute.For<IArticleSearchEngine>();
            var emptyResultSet = new List<Article>();
            stubSearchEngine
                .PerformSearch(Arg.Any<SearchSettings>())
                .Returns(emptyResultSet);

            var stubDialogService = Substitute.For<IMessageBoxDialogService>();
            var notifier = new SearchEngineNoResultFoundNotifier(
                stubSearchEngine,
                stubDialogService);

            notifier.PerformSearch(new SearchSettings());

            stubDialogService.Received().Show(
                Arg.Any<string>(), 
                Arg.Any<string>(), 
                Arg.Any<DialogButton>());
        }

        [Test]
        public void PerformSearch_ResultFound_DoesNotNotifyUser()
        {
            var stubSearchEngine = Substitute.For<IArticleSearchEngine>();
            var notEmptyResultSet = new List<Article> { CreateStubArticle() };
            stubSearchEngine
                .PerformSearch(Arg.Any<SearchSettings>())
                .Returns(notEmptyResultSet);

            var stubDialogService = Substitute.For<IMessageBoxDialogService>();
            var notifier = new SearchEngineNoResultFoundNotifier(
                stubSearchEngine,
                stubDialogService);

            notifier.PerformSearch(new SearchSettings());

            stubDialogService.DidNotReceive().Show(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DialogButton>());
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
    }
}
