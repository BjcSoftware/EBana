using EBana.Models;
using System;

namespace EBana.ArticlePictures
{
    public interface IArticlePictureUpdater
    {
        void UpdatePictureOfArticle(Article articleToUpdate, Uri newPictureLocation);
    }
}
