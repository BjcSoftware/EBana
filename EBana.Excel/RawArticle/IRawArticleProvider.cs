using System.Collections.Generic;

namespace EBana.Excel
{
    /// <summary>
    /// Représente une source de RawArticle.
    /// </summary>
    public interface IRawArticleProvider
    {
        List<RawArticle> GetRawArticlesFrom(string source);
    }
}
