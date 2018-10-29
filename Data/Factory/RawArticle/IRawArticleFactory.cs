using System.Collections.Generic;

using EBana.Models;

namespace EBana.Factory
{
    public interface IRawArticleFactory
    {
        List<RawArticle> CreateAllRawArticles();
    }
}
