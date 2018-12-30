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
        private readonly IArticlePictureFileNameFormatter fileNameFormater;
        private readonly ArticlePictureSettings pictureSettings;

        private List<string> availableArticlePictureCache;

        public ArticlePictureLocator(
            IFileService fileService,
            IArticlePictureFileNameFormatter fileNameFormater, 
            ArticlePictureSettings pictureSettings)
        {
            if (fileService == null)
                throw new ArgumentNullException("fileService");
            if (fileNameFormater == null)
                throw new ArgumentNullException("fileNameFormater");
            if (pictureSettings == null)
                throw new ArgumentNullException("pictureSettings");

            this.fileService = fileService;
            this.fileNameFormater = fileNameFormater;
            this.pictureSettings = pictureSettings;

            availableArticlePictureCache = new List<string>();

            CreatePictureFolderIfDoesNotExist();
        }

        private void CreatePictureFolderIfDoesNotExist()
        {
            string pictureFolderPath = pictureSettings.PictureFolderPath;
            if (!fileService.DirectoryExists(pictureFolderPath))
            {
                fileService.CreateDirectory(pictureFolderPath);
            }
        }

        public Uri GetArticlePictureLocationOrDefault(Article articleToLocate)
        {
            string pictureFileName = GetArticlePictureFileNameOrDefault(articleToLocate);
            string picturePath = $"{pictureSettings.PictureFolderPath}/{pictureFileName}";
            Uri pictureLocation = new Uri(picturePath, UriKind.Relative);
            return pictureLocation;
        }

        private string GetArticlePictureFileNameOrDefault(Article article)
        {
            string fileName =  IsArticleHavingAPicture(article) ?
                GetPictureFileNameFromArticle(article) : GetDefaultPictureFileName();

            return fileName;
        }

        public bool IsArticleHavingAPicture(Article article)
        {
            if (article != null)
            {
                var existingPictureNames = GetExistingPictureNames();
                string potentialPictureName = GetPictureFileNameFromArticle(article);
                return existingPictureNames.Contains(potentialPictureName);
            }

            return false;
        }

        private IEnumerable<string> GetExistingPictureNames()
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
            var availablePictureNames = fileService
                .GetAllFileNamesInFolder(pictureSettings.PictureFolderPath);

            availableArticlePictureCache.Clear();
            availableArticlePictureCache.AddRange(availablePictureNames);
        }

        private string GetPictureFileNameFromArticle(Article article)
        {
            return fileNameFormater.Format(article);
        }

        private string GetDefaultPictureFileName()
        {
            return fileNameFormater.FormatDefault();
        }

        public Uri GetPictureFolderLocation()
        {
            string folderPath = pictureSettings.PictureFolderPath;
            Uri pictureFolderLocation = new Uri(folderPath, UriKind.Relative);
            return pictureFolderLocation;
        }

        public void ClearPictureCache()
        {
            availableArticlePictureCache.Clear();
        }
    }
}
