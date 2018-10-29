using System.Collections.Generic;

using EBana.Models;

namespace EBana
{
    public interface IArticleFactory
    {
        List<Article> CreateAllArticles();
    }
}
