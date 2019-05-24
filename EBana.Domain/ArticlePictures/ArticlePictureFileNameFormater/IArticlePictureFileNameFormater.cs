using EBana.Domain.Models;

namespace EBana.Domain.ArticlePictures
{
    public interface IArticlePictureNameFormater
    {
        string Format(Article article);

        /// <summary>
        /// Donner le nom de l'image d'article par défaut
        /// </summary>
        string FormatDefault();
    }
}
