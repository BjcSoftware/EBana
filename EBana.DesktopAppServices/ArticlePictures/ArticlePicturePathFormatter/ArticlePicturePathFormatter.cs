using System;
using EBana.Domain.Models;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public class ArticlePicturePathFormatter : IArticlePicturePathFormatter
    {
        private readonly ArticlePictureSettings settings;
        private readonly IArticlePictureNameFormatter formatter;

        public ArticlePicturePathFormatter(
            ArticlePictureSettings settings,
            IArticlePictureNameFormatter formatter)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            this.settings = settings;
            this.formatter = formatter;
        }

        public string FormatPath(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            string fileName = settings.FormatPictureFileName(formatter.FormatName(article));
            return $"{settings.PictureFolderPath}/{fileName}";
        }
    }
}
