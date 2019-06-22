using System;
using EBana.Domain;
using EBana.Domain.ArticlePictures;
using EBana.Domain.ArticlePictures.Events;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.DesktopAppServices.ArticlePictures
{
    public class ArticlePictureUpdater : IArticlePictureUpdater
    {
        private readonly IFileService fileService;
        private readonly IArticlePicturePathFormatter formatter;
        private readonly IEventHandler<ArticlePictureUpdated> articlePictureUpdated;

        public ArticlePictureUpdater(
            IFileService fileService, 
            IArticlePicturePathFormatter formatter,
            IEventHandler<ArticlePictureUpdated> articlePictureUpdated)
        {
            if (fileService == null)
                throw new ArgumentNullException(nameof(fileService));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));
            if (articlePictureUpdated == null)
                throw new ArgumentNullException(nameof(articlePictureUpdated));

            this.fileService = fileService;
            this.formatter = formatter;
            this.articlePictureUpdated = articlePictureUpdated;
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

            articlePictureUpdated.Handle(
                new ArticlePictureUpdated(articleToUpdate));
        }
    }
}
