using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.SearchEngine
{
    /// <summary>
    /// Permet de fournir les critères de recherche disponibles.
    /// </summary>
    public interface ISearchSettingsProvider
    {
        IEnumerable<TypeArticle> GetArticleTypes();
        IEnumerable<TypeEpi> GetEpiTypes();
    }
}
