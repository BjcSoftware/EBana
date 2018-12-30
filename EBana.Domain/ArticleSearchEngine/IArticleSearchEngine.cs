using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.SearchEngine
{
    public interface IArticleSearchEngine
    {
        /// <summary>
        /// Rechercher les articles
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        IEnumerable<Article> SearchArticles(string searchQuery);

        /// <summary>
        /// Rechercher les articles banalisés
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        IEnumerable<Article> SearchBanalise(string searchQuery);

        /// <summary>
        /// Rechercher les articles EPI de type <paramref name="type"/>
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        IEnumerable<Article> SearchEpi(string searchQuery, TypeEpi type);

        /// <summary>
        /// Rechercher les articles SEL
        /// qui possèdent un libellé ou une référence qui contient <paramref name="searchQuery"/>
        /// </summary>
        IEnumerable<Article> SearchSel(string searchQuery);
    }
}
