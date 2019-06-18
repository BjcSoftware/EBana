using System;
using EBana.Domain.ArticlePictures;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public class ArticlePictureUpdater : IArticlePictureUpdater
    {
        private readonly IFileService fileService;
        private readonly IArticlePicturePathFormatter formatter;
        private readonly ArticlePictureSettings settings;

        public ArticlePictureUpdater(
            IFileService fileService, 
            IArticlePicturePathFormatter formatter,
            ArticlePictureSettings settings)
        {
            if (fileService == null)
                throw new ArgumentNullException(nameof(fileService));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            this.fileService = fileService;
            this.formatter = formatter;
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
            string newPictureLocation)
        {
            if (articleToUpdate == null)
                throw new ArgumentNullException(nameof(articleToUpdate));
            if (newPictureLocation == null)
                throw new ArgumentNullException(nameof(newPictureLocation));

            fileService.Copy(
                newPictureLocation,
                formatter.FormatPath(articleToUpdate));
        }
    }
}
