using EBana.Domain.Models;
using System;

namespace EBana.Domain.ArticlePictures
{
    public interface IArticlePictureUpdater
    {
        void UpdatePictureOfArticle(Article articleToUpdate, string newPictureLocation);
    }
}
