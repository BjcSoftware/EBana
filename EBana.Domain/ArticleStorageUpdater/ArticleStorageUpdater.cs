﻿using EBana.Domain.Models;
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
                throw new ArgumentNullException("articleRepository");

            this.articleRepository = articleRepository;
        }
		
		public void ReplaceAvailableArticlesWith(IEnumerable<Article> articles)
        {
            articleRepository.RemoveAll();
            articleRepository.AddRange(articles);
        }
	}
}
