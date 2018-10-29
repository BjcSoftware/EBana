﻿using EBana.Models;
using System;

namespace EBana.ArticlePictures
{
    public interface IArticlePictureLocator
    {
        /// <summary>
        /// Récupérer l'emplacement de la photo d'un article
        /// </summary>
        /// <returns>Emplacement de la photo de l'article s'il en a une, sinon la photo par défaut</returns>
        Uri GetArticlePictureLocationOrDefault(Article articleToLocate);

        Uri GetPictureFolderLocation();

        bool IsArticleHavingAPicture(Article article);

        void ClearPictureCache();
    }
}
