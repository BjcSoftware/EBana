﻿using System;
using System.Collections.Generic;
using EBana.Models;
using OsServices.File;

namespace EBana.ArticlePictures
{
    public class ArticlePictureLocator : IArticlePictureLocator
    {
        private readonly IFileService fileService;
        private readonly IArticlePictureFileNameFormatter fileNameFormater;
        private readonly ArticlePictureSettings pictureSettings;

        private List<string> availableArticlePictureCache;

        public ArticlePictureLocator(IFileService fileService,
            IArticlePictureFileNameFormatter fileNameFormater, 
            ArticlePictureSettings pictureSettings)
        {
            this.fileService = fileService;
            this.fileNameFormater = fileNameFormater;
            this.pictureSettings = pictureSettings;

            availableArticlePictureCache = new List<string>();
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
            if(article != null)
            {
                if (availableArticlePictureCache.Count == 0)
                {
                    var availablePictureNames = fileService.GetAllFileNamesInFolder(pictureSettings.PictureFolderPath);
                    availableArticlePictureCache.AddRange(availablePictureNames);
                }

                string potentialPictureName = GetPictureFileNameFromArticle(article);
                return availableArticlePictureCache.Contains(potentialPictureName);
            }

            return false;
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
