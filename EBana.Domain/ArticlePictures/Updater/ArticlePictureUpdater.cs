using System;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureUpdater : IArticlePictureUpdater
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureFileNameFormatter pictureFileNameFormatter;
        private readonly ArticlePictureSettings articlePictureSettings;

        public ArticlePictureUpdater(
            IFileService fileService, 
            IArticlePictureFileNameFormatter pictureFileNameFormatter,
            ArticlePictureSettings articlePictureSettings)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            if (pictureFileNameFormatter == null)
                throw new ArgumentNullException("pictureFileNameFormatter");
            if (articlePictureSettings == null)
                throw new ArgumentNullException("articlePictureSettings");

            this.fileService = fileService;
            this.pictureFileNameFormatter = pictureFileNameFormatter;
            this.articlePictureSettings = articlePictureSettings;

            CreatePictureFolderIfDoesNotExist();
        }

        private void CreatePictureFolderIfDoesNotExist()
        {
            string pictureFolderPath = articlePictureSettings.PictureFolderPath;
            if (!fileService.DirectoryExists(pictureFolderPath))
            {
                fileService.CreateDirectory(pictureFolderPath);
            }
        }

        public void UpdatePictureOfArticle(
            Article articleToUpdate, 
            Uri newPictureLocation)
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
