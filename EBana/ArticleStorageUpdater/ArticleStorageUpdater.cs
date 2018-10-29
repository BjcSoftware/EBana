using Data.Repository;
using EBana.Models;
using System.Collections.Generic;

namespace EBana
{
    /// <summary>
    /// Permet de mettre à jour les articles disponibles
    /// </summary>
	public class ArticleStorageUpdater
    {
        private readonly IArticleFactory articleFactory;
        private readonly IWriter<Article> articleWriter;
        private readonly IWriter<TypeEpi> typeEpiWriter;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="articleFactory">Source de la mise à jour (d'où viennent les nouveaux articles)</param>
        /// <param name="articleWriter">Accès au stockage des articles</param>
        /// <param name="typeEpiWriter">Accès au stockage des types d'EPIs disponibles</param>
        public ArticleStorageUpdater(IArticleFactory articleFactory, IWriter<Article> articleWriter, IWriter<TypeEpi> typeEpiWriter)
        {
			this.articleFactory = articleFactory;
            this.articleWriter = articleWriter;
            this.typeEpiWriter = typeEpiWriter;
        }
		
		public void Update()
        {
            RemoveOldArticles();
            AddNewArticles();
            SaveChanges();
        }

        private void RemoveOldArticles()
        {
            articleWriter.RemoveAll();
            typeEpiWriter.RemoveAll();
        }

        private void AddNewArticles()
        {
            IEnumerable<Article> articlesToAdd = articleFactory.CreateAllArticles();
            articleWriter.AddRange(articlesToAdd);
        }

        private void SaveChanges()
        {
            articleWriter.Save();
        }
	}
}
