using EBana.Domain;
using EBana.Domain.Models;
using System;
using System.Collections.Generic;

namespace EBana.Excel
{
    /// <summary>
    /// Permet d'utiliser un fichier Excel comme source d'articles.
    /// </summary>
    public class ExcelArticleProvider : IArticleProvider
    {
        private List<RawArticle> rawArticles;
        private int createdArticlesCount;

        private readonly IRawArticleProvider rawArticleProvider;
        private readonly IRawArticleToArticleMapper rawArticleToArticleMapper;

        public ExcelArticleProvider(
            IRawArticleProvider rawArticleProvider,
            IRawArticleToArticleMapper rawArticleToArticleMapper)
        {
            if (rawArticleProvider == null)
                throw new ArgumentNullException("rawArticleProvider");
            if (rawArticleToArticleMapper == null)
                throw new ArgumentNullException("rawArticleToArticleMapper");

            this.rawArticleToArticleMapper = rawArticleToArticleMapper;
            this.rawArticleProvider = rawArticleProvider;
        }

        public IEnumerable<Article> GetArticles(string source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            rawArticles = rawArticleProvider.GetRawArticles(source);

            createdArticlesCount = 0;
            List<Article> articles = new List<Article>(rawArticles.Count);
            while (ThereIsStillArticleToGet())
            {
                articles.Add(GetNextArticle());
            }

            return articles;
        }

        private bool ThereIsStillArticleToGet()
        {
            return createdArticlesCount < rawArticles.Count;
        }

        private Article GetNextArticle()
        {
            RawArticle rawArticle = rawArticles[createdArticlesCount++];
            Article createdArticle = rawArticleToArticleMapper.Map(rawArticle);
            return createdArticle;
        }
    }
}
