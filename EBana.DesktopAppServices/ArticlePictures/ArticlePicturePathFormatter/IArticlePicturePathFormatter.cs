using EBana.Domain.Models;

namespace EBana.DesktopAppServices.ArticlePictures
{
    public interface IArticlePicturePathFormatter
    {
        string FormatPath(Article article);
    }
}
