using EBana.Domain.Models;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public interface IArticlePictureNameFormatter
    {
        string FormatName(Article article);

        /// <summary>
        /// Donner le nom de l'image d'article par défaut.
        /// </summary>
        string FormatDefaultName();
    }
}
