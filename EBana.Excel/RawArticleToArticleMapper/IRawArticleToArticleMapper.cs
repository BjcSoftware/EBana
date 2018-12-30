using EBana.Domain.Models;

namespace EBana.Excel
{
    /// <summary>
    /// Permet de créer un Article à partir d'un RawArticle.
    /// </summary>
    public interface IRawArticleToArticleMapper
    {
        Article Map(RawArticle rawArticle);
    }
}
