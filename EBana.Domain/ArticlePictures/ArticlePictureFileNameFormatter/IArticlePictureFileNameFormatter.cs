using EBana.Domain.Models;

namespace EBana.Domain.ArticlePictures
{
    public interface IArticlePictureFileNameFormatter
    {
        string Format(Article article);

        // donner le nom de l'image d'article par défaut
        string FormatDefault();
    }
}
