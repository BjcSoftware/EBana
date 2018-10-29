using System.Collections.Generic;
using EBana.Models;
using Data;

namespace EBana.Factory
{
	public class ArticleFromRawArticleFactory : IArticleFactory
	{
        public ArticleFromRawArticleFactory(IRawArticleFactory rawArticleFactory)
        {
            IArticlesSettings settings = new ArticlesSettings();
            rawArticleToArticleMapper = new RawArticleToArticleMapper(settings);

            rawArticles = rawArticleFactory.CreateAllRawArticles();
        }

        public List<Article> CreateAllArticles()
        {
            List<Article> articles = new List<Article>();

            while(ThereIsStillArticleToCreate())
            {
                articles.Add(CreateNextArticle());
            }

            return articles;
        }

        private bool ThereIsStillArticleToCreate()
        {
            return createdArticlesCount < rawArticles.Count;
        }

        private Article CreateNextArticle()
        {
            RawArticle rawArticle = rawArticles[createdArticlesCount++];
            Article createdArticle = rawArticleToArticleMapper.Map(rawArticle);
            return createdArticle;
        }

        private List<RawArticle> rawArticles;
        private RawArticleToArticleMapper rawArticleToArticleMapper;
        private int createdArticlesCount;
    }
}