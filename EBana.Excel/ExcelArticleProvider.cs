using EBana.Domain;
using EBana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBana.Excel
{
    /// <summary>
    /// Permet d'utiliser un fichier Excel comme source d'articles.
    /// </summary>
    public class ExcelArticleProvider : IArticleProvider
    {
        private readonly IRawArticleProvider rawArticleProvider;
        private readonly IRawArticleToArticleMapper rawArticleToArticleMapper;

        public ExcelArticleProvider(
            IRawArticleProvider rawArticleProvider,
            IRawArticleToArticleMapper rawArticleToArticleMapper)
        {
            if (rawArticleProvider == null)
                throw new ArgumentNullException(nameof(rawArticleProvider));
            if (rawArticleToArticleMapper == null)
                throw new ArgumentNullException(nameof(rawArticleToArticleMapper));
            
            this.rawArticleProvider = rawArticleProvider;
            this.rawArticleToArticleMapper = rawArticleToArticleMapper;
        }

        /// <summary>
        /// Lire les articles du fichier Excel <source>
        /// </summary>
        public IEnumerable<Article> GetArticlesFrom(string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return 
                from rawArticle in rawArticleProvider.GetRawArticlesFrom(source)
                select rawArticleToArticleMapper.Map(rawArticle);
        }
    }
}
