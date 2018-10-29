using EBana.Models;

namespace EBana.ArticlePictures
{
    public class ArticlePictureFileNameFormatter : IArticlePictureFileNameFormatter
    {
        private readonly ArticlePictureSettings pictureSettings;
        public ArticlePictureFileNameFormatter(ArticlePictureSettings pictureSettings)
        {
            this.pictureSettings = pictureSettings;
        }

        public string Format(Article article)
        {
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
