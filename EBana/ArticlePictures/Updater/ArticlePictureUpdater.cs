using System;
using EBana.Models;
using OsServices.File;

namespace EBana.ArticlePictures
{
    public class ArticlePictureUpdater : IArticlePictureUpdater
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureFileNameFormatter pictureFileNameFormatter;
        private readonly ArticlePictureSettings articlePictureSettings;

        public ArticlePictureUpdater(IFileService fileService, 
            IArticlePictureFileNameFormatter pictureFileNameFormatter,
            ArticlePictureSettings articlePictureSettings)
        {
            this.fileService = fileService;
            this.pictureFileNameFormatter = pictureFileNameFormatter;
            this.articlePictureSettings = articlePictureSettings;
        }

        public void UpdatePictureOfArticle(Article articleToUpdate, Uri newPictureLocation)
        {
            string pictureFolderPath = articlePictureSettings.PictureFolderPath;
            string pictureFileName = GetPictureFileNameFromArticle(articleToUpdate);
            string updatedPictureLocation = $"{pictureFolderPath}/{pictureFileName}";

            fileService.Copy(
                newPictureLocation.OriginalString, 
                updatedPictureLocation);
        }

        private string GetPictureFileNameFromArticle(Article article)
        {
            return pictureFileNameFormatter.Format(article);
        }
    }
}
