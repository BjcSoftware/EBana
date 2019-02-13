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
            fileService.Copy(
                newPictureLocation.OriginalString,
                GetPictureLocation(articleToUpdate));
        }

        private string GetPictureLocation(Article article)
        {
            string pictureFolderPath = articlePictureSettings.PictureFolderPath;
            string pictureFileName = GetPictureFileNameFromArticle(article);
            string pictureLocation = $"{pictureFolderPath}/{pictureFileName}";
            return pictureLocation;
        }

        private string GetPictureFileNameFromArticle(Article article)
        {
            return pictureFileNameFormatter.Format(article);
        }
    }
}
