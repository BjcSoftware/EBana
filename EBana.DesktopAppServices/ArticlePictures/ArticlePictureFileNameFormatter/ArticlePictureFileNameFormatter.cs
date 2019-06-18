using EBana.Domain.Models;
using System;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public class ArticlePictureNameFormatter : IArticlePictureNameFormatter
    {
        private readonly ArticlePictureSettings pictureSettings;

        public ArticlePictureNameFormatter(
            ArticlePictureSettings pictureSettings)
        {
            if (pictureSettings == null)
                throw new ArgumentNullException(nameof(pictureSettings));

            this.pictureSettings = pictureSettings;
        }

        public string FormatName(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            return article.Ref;
        }

        public string FormatDefaultName()
        {
            return pictureSettings.DefaultPictureName;
        }
    }
}
