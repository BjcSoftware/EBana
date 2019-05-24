using System;
using System.Collections.Generic;
using System.Linq;
using EBana.Domain.Models;
using EBana.Services.File;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureLocator : IArticlePictureLocator
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureNameFormater nameFormater;
        private readonly ArticlePictureSettings settings;

        private List<string> availableArticlePictureCache;

        public ArticlePictureLocator(
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

            availableArticlePictureCache = new List<string>();

            CreatePictureFolderIfDoesNotExist();
        }

        private void CreatePictureFolderIfDoesNotExist()
        {
            string pictureFolderPath = settings.PictureFolderPath;
            if (!fileService.DirectoryExists(pictureFolderPath))
            {
                fileService.CreateDirectory(pictureFolderPath);
            }
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
                nameFormater.Format(article));
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
