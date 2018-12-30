using System;
using System.Collections.Generic;
using EBana.Domain.Models;
using EBana.Domain.Repository;

namespace EBana.EfDataAccess.Repository
{
    /// <summary>
    /// Permet de réaliser des opérations sur les articles stockés.
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly IWriter<Article> articleWriter;
        private readonly IWriter<TypeEpi> typeEpiWriter;

        public ArticleRepository(
            IWriter<Article> articleWriter,
            IWriter<TypeEpi> typeEpiWriter)
        {
            if (articleWriter == null)
                throw new ArgumentNullException("articleWriter");
            if (typeEpiWriter == null)
                throw new ArgumentNullException("typeEpiReader");

            this.articleWriter = articleWriter;
            this.typeEpiWriter = typeEpiWriter;
        }

        public void AddRange(IEnumerable<Article> articlesToAdd)
        {
            if (articlesToAdd == null)
                throw new ArgumentNullException("articlesToAdd");

            articleWriter.AddRange(articlesToAdd);
            articleWriter.Save();
        }

        public void RemoveAll()
        {
            articleWriter.RemoveAll();
            typeEpiWriter.RemoveAll();

            // seul un save sur articleWriter est suffisant car articleWriter et typeEpiWriter 
            // doivent partager le même DbContext
            articleWriter.Save();
        }
    }
}
