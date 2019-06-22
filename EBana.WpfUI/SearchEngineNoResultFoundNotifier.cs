using EBana.Domain.Models;
using EBana.Domain.SearchEngine;
using EBana.Services.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBana.WpfUI
{
    public class SearchEngineNoResultFoundNotifier : IArticleSearchEngine
    {
        private readonly IArticleSearchEngine decoratedSearchEngine;
        private readonly IMessageBoxDialogService dialogService;

        public SearchEngineNoResultFoundNotifier(
            IArticleSearchEngine decoratedSearchEngine,
            IMessageBoxDialogService dialogService)
        {
            if (decoratedSearchEngine == null)
                throw new ArgumentNullException(nameof(decoratedSearchEngine));
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            this.decoratedSearchEngine = decoratedSearchEngine;
            this.dialogService = dialogService;
        }

        public IEnumerable<Article> PerformSearch(SearchSettings settings)
        {
            var results = decoratedSearchEngine.PerformSearch(settings);
            if(!results.Any())
            {
                NotifyUserThatNoResultFound();
            }

            return results;
        }

        private void NotifyUserThatNoResultFound()
        {
            dialogService.Show(
                "Information",
                "Aucun résultat ne correspond à vos critères de recherche.",
                DialogButton.Ok);
        }
    }
}
