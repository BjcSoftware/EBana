using EBana.Domain.Models;

namespace EBana.Domain.ArticlePictures.Events
{
    public class ArticlePictureUpdated
    {
        public readonly Article Article;

        public ArticlePictureUpdated(Article article)
        {
            this.Article = article;
        }
    }
}
