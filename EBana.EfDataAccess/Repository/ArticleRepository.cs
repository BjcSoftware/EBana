using System;
using System.Collections.Generic;
using System.Linq;
using EBana.Domain.Models;
using EBana.Domain.Repository;

namespace EBana.EfDataAccess.Repository
{
    /// <summary>
    /// Permet de réaliser des opérations sur les articles stockés.
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly IWriter<Article> articleWriter;

        public ArticleRepository(
            IWriter<Article> articleWriter)
        {
            if (articleWriter == null)
                throw new ArgumentNullException(nameof(articleWriter));

            this.articleWriter = articleWriter;
        }

        public void AddRange(IEnumerable<Article> articlesToAdd)
        {
            if (articlesToAdd == null)
                throw new ArgumentNullException(nameof(articlesToAdd));

            articleWriter.AddRange(articlesToAdd);
            articleWriter.Save();
        }

        public void RemoveAll()
        {
            articleWriter.RemoveAll();
            articleWriter.Save();
        }
    }
}
