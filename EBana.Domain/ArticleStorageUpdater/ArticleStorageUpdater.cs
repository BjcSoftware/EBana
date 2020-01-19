using EBana.Domain.ArticleStorageUpdater.Event;
using EBana.Domain.Models;
using EBana.Domain.Repository;
using System;
using System.Collections.Generic;

namespace EBana.Domain.ArticleStorageUpdater
{
	public class ArticleStorageUpdater : IArticleStorageUpdater
	{
		private readonly IArticleRepository articleRepository;
        private IEventHandler<ArticleStorageUpdated> handler;

        public ArticleStorageUpdater(
            IArticleRepository articleRepository,
            IEventHandler<ArticleStorageUpdated> handler)
		{
			if (articleRepository == null)
				throw new ArgumentNullException(nameof(articleRepository));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

			this.articleRepository = articleRepository;
            this.handler = handler;
		}
		
		public void ReplaceAvailableArticlesWith(
            IEnumerable<Article> newArticles)
		{
            if (newArticles == null)
                throw new ArgumentNullException(nameof(newArticles));

			articleRepository.RemoveAll();
			articleRepository.AddRange(newArticles);

            handler.Handle(new ArticleStorageUpdated());
		}
	}
}
