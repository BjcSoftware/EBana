using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.SearchEngine
{
    /// <summary>
    /// Permet de fournir les critères de recherche disponibles.
    /// </summary>
    public interface ISearchSettingsProvider
    {
        /// <summary>
        /// Récupérer les types d'articles disponibles.
        /// </summary>
        IEnumerable<TypeArticle> GetArticleTypes();

        /// <summary>
        /// Récupérer les types d'EPIs disponibles.
        /// </summary>
        IEnumerable<TypeEpi> GetEpiTypes();
    }
}
