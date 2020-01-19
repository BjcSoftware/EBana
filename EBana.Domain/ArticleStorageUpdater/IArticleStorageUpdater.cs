using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.ArticleStorageUpdater
{
    /// <summary>
    /// Permet de mettre à jour les articles disponibles.
    /// </summary>
    public interface IArticleStorageUpdater
    {
        void ReplaceAvailableArticlesWith(IEnumerable<Article> newArticles);
    }
}
