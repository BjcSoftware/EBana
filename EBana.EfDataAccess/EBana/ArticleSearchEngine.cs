using EBana.EfDataAccess.Repository;
using EBana.Domain.Models;
using System;
using System.Collections.Generic;
using EBana.Domain.SearchEngine;
using System.Linq;

namespace EBana.EfDataAccess
{
    public class ArticleSearchEngine : IArticleSearchEngine
    {
        private readonly IReader<Article> articleReader;
        private readonly IReader<Banalise> banaliseReader;
        private readonly IReader<EPI> epiReader;
        private readonly IReader<SEL> selReader;

        public ArticleSearchEngine(
            IReader<Article> articleReader,
            IReader<Banalise> banaliseReader,
            IReader<EPI> epiReader,
            IReader<SEL> selReader)
        {
            if (articleReader == null)
                throw new ArgumentNullException(nameof(articleReader));
            if (banaliseReader == null)
                throw new ArgumentNullException(nameof(banaliseReader));
            if (epiReader == null)
                throw new ArgumentNullException(nameof(epiReader));
            if (selReader == null)
                throw new ArgumentNullException(nameof(selReader));

            this.articleReader = articleReader;
            this.banaliseReader = banaliseReader;
            this.epiReader = epiReader;
            this.selReader = selReader;
        }

        public IEnumerable<Article> PerformSearch(SearchSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            string articleTypeFilter = settings.ArticleTypeFilter?.Libelle;
            switch (articleTypeFilter)
            {
                case null:
                    return SearchArticles(settings.Query);
                case "Banalisé":
                    return SearchBanalise(settings);
                case "SEL":
                    return SearchSel(settings.Query);
                default:
                    throw new InvalidOperationException($"Invalid article type: {settings.ArticleTypeFilter}");
            }
        }

        /// <summary>
        /// Rechercher les articles
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        private IEnumerable<Article> SearchArticles(string searchQuery)
        {
            if (searchQuery == null)
                throw new ArgumentNullException(nameof(searchQuery));

            searchQuery = searchQuery.ToLower();
            return articleReader
                .Find(a => a.Libelle.ToLower().Contains(searchQuery) ||
                            a.Ref.ToLower().Contains(searchQuery))
                .OrderBy(a => a.Ref);
        }

        private IEnumerable<Article> SearchBanalise(SearchSettings settings)
        {
            if (settings.IsEpi)
            {
                return SearchEpi(settings.Query, settings.EpiTypeFilter);
            }
            return SearchBanalise(settings.Query);
        }

        /// <summary>
        /// Rechercher les articles banalisés
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        private IEnumerable<Article> SearchBanalise(string searchQuery)
        {
            if (searchQuery == null)
                throw new ArgumentNullException(nameof(searchQuery));

            searchQuery = searchQuery.ToLower();
            return banaliseReader
                .Find(a => a.Libelle.ToLower().Contains(searchQuery) ||
                            a.Ref.ToLower().Contains(searchQuery))
                .OrderBy(a => a.Ref);
        }

        /// <summary>
        /// Rechercher les articles EPI de type <paramref name="type"/>
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        private IEnumerable<Article> SearchEpi(string searchQuery, TypeEpi type)
        {
            if (searchQuery == null)
                throw new ArgumentNullException(nameof(searchQuery));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            searchQuery = searchQuery.ToLower();
            return epiReader
                .Find(a => (a.Libelle.ToLower().Contains(searchQuery) ||
                            a.Ref.ToLower().Contains(searchQuery)) &&
                            a.TypeEpi.Equals(type))
                .OrderBy(a => a.Ref);
        }

        /// <summary>
        /// Rechercher les articles SEL
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        private IEnumerable<Article> SearchSel(string searchQuery)
        {
            if (searchQuery == null)
                throw new ArgumentNullException(nameof(searchQuery));

            searchQuery = searchQuery.ToLower();
            return selReader
                .Find(a => a.Libelle.ToLower().Contains(searchQuery) ||
                            a.Ref.ToLower().Contains(searchQuery))
                .OrderBy(a => a.Ref);
        }
    }
}
