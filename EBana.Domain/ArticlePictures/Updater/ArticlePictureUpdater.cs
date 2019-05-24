using System;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureUpdater : IArticlePictureUpdater
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureNameFormater nameFormater;
        private readonly ArticlePictureSettings settings;

        public ArticlePictureUpdater(
            IFileService fileService, 
            IArticlePictureNameFormater nameFormater,
            ArticlePictureSettings settings)
        {
            if (fileService == null)
                throw new ArgumentNullException(nameof(fileService));
            if (nameFormater == null)
                throw new ArgumentNullException(nameof(nameFormater));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            this.fileService = fileService;
            this.nameFormater = nameFormater;
            this.settings = settings;

            CreatePictureFolderIfDoesNotExist();
        }

        private void CreatePictureFolderIfDoesNotExist()
        {
            if (!fileService.DirectoryExists(settings.PictureFolderPath))
            {
                fileService.CreateDirectory(settings.PictureFolderPath);
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
            return settings.FormatPicturePath(GetPictureNameFor(article));
        }

        private string GetPictureNameFor(Article article)
        {
            return nameFormater.Format(article);
        }
    }
}
