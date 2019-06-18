using EBana.Domain.Models;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public interface IArticlePicturePathFormatter
    {
        string FormatPath(Article article);
    }
}
