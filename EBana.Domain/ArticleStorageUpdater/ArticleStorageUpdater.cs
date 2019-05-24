using EBana.Domain.Models;
using EBana.Domain.Repository;
using System;
using System.Collections.Generic;

namespace EBana.Domain
{
	public class ArticleStorageUpdater : IArticleStorageUpdater
	{
		private readonly IArticleRepository articleRepository;

		public ArticleStorageUpdater(IArticleRepository articleRepository)
		{
			if (articleRepository == null)
				throw new ArgumentNullException(nameof(articleRepository));

			this.articleRepository = articleRepository;
		}
		
		public void ReplaceAvailableArticlesWith(IEnumerable<Article> newArticles)
		{
			articleRepository.RemoveAll();
			articleRepository.AddRange(newArticles);
		}
	}
}
