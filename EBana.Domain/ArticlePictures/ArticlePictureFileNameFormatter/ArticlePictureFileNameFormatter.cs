using EBana.Domain.Models;
using System;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureFileNameFormatter : IArticlePictureFileNameFormatter
    {
        private readonly ArticlePictureSettings pictureSettings;

        public ArticlePictureFileNameFormatter(
            ArticlePictureSettings pictureSettings)
        {
            if (pictureSettings == null)
                throw new ArgumentNullException("pictureSettings");

            this.pictureSettings = pictureSettings;
        }

        public string Format(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            string name = article.Ref;
            string fileExtension = pictureSettings.PictureFileExtension;
            string fileName = $"{name}.{fileExtension}";

            return fileName;
        }

        public string FormatDefault()
        {
            string defaultName = pictureSettings.DefaultPictureName;
            string fileExtension = pictureSettings.PictureFileExtension;
            string fileName = $"{defaultName}.{fileExtension}";

            return fileName;
        }
    }
}
