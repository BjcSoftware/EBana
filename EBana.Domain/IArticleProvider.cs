using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain
{
    /// <summary>
    /// Représente une source d'articles.
    /// </summary>
    public interface IArticleProvider
    {
        IEnumerable<Article> GetArticlesFrom(string source);
    }
}
