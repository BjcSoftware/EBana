using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.Repository
{
    /// <summary>
    /// Permet de réaliser des opérations sur les articles stockés.
    /// </summary>
    public interface IArticleRepository
    {
        void AddRange(IEnumerable<Article> articlesToAdd);
        void RemoveAll();
    }
}
