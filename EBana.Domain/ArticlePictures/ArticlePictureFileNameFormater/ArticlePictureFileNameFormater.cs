using EBana.Domain.Models;
using System;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureNameFormater : IArticlePictureNameFormater
    {
        private readonly ArticlePictureSettings pictureSettings;

        public ArticlePictureNameFormater(
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

            return article.Ref;
        }

        public string FormatDefault()
        {
            return pictureSettings.DefaultPictureName;
        }
    }
}
