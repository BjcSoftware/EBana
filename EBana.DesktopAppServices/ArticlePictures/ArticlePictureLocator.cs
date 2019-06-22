using System;
using System.Collections.Generic;
using System.Linq;
using EBana.Domain.ArticlePictures;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.DesktopAppServices.ArticlePictures
{
    public class ArticlePictureLocator : IArticlePictureLocator
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureNameFormatter nameFormatter;
        private readonly ArticlePictureSettings settings;

        private List<string> availableArticlePictureCache;

        public ArticlePictureLocator(
            IFileService fileService,
            IArticlePictureNameFormatter nameFormatter, 
            ArticlePictureSettings settings)
        {
            if (fileService == null)
                throw new ArgumentNullException(nameof(fileService));
            if (nameFormatter == null)
                throw new ArgumentNullException(nameof(nameFormatter));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            this.fileService = fileService;
            this.nameFormatter = nameFormatter;
            this.settings = settings;

            availableArticlePictureCache = new List<string>();
        }

        public Uri GetArticlePictureLocationOrDefault(Article articleToLocate)
        {
            string pictureFileName = GetArticlePictureFileNameOrDefault(articleToLocate);
            string picturePath = $"{settings.PictureFolderPath}/{pictureFileName}";
            return new Uri(picturePath, UriKind.Relative);
        }

        private string GetArticlePictureFileNameOrDefault(Article article)
        {
            return IsArticleHavingAPicture(article) ?
                GetPictureFileNameFor(article) : GetDefaultPictureFileName();
        }

        public bool IsArticleHavingAPicture(Article article)
        {
            if (article == null) return false;
            
            return ExistingPictureFileNames()
                .Contains(GetPictureFileNameFor(article));
        }

        private IEnumerable<string> ExistingPictureFileNames()
        {
            // on utilise ici un système de cache pour limiter le nombre d'appels systèmes (qui peuvent être lents)
            if (ArticlePictureCacheIsEmpty())
            {
                UpdateArticlePictureCache();
            }

            return availableArticlePictureCache;
        }

        private bool ArticlePictureCacheIsEmpty()
        {
            return availableArticlePictureCache.Count == 0;
        }

        private void UpdateArticlePictureCache()
        {
            availableArticlePictureCache.Clear();
            availableArticlePictureCache
                .AddRange(AvailableArticlePictures());
        }

        private IEnumerable<string> AvailableArticlePictures()
        {
            return fileService
                .GetAllFileNamesInFolder(settings.PictureFolderPath)
                .Where(f => settings.IsCorrectPictureFile(f));
        }

        private string GetPictureFileNameFor(Article article)
        {
            return settings.FormatPictureFileName(
                nameFormatter.FormatName(article));
        }

        private string GetDefaultPictureFileName()
        {
            return settings.FormatDefaultPictureFileName();
        }

        public Uri PictureFolderLocation => new Uri(settings.PictureFolderPath, UriKind.Relative);

        public void ClearPictureCache()
        {
            availableArticlePictureCache.Clear();
        }
    }
}
