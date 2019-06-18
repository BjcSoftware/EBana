using EBana.Domain.Models;
using System.Collections.Generic;

namespace EBana.Domain.SearchEngine
{
    public interface IArticleSearchEngine
    {
        IEnumerable<Article> PerformSearch(SearchSettings settings);
    }
}
