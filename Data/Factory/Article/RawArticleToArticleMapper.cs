using EBana.Models;
using Data;

namespace EBana.Factory
{
    public class RawArticleToArticleMapper
    {
        public RawArticleToArticleMapper(IArticlesSettings articlesSettings)
        {
            this.articlesSettings = articlesSettings;
        }

        public Article Map(RawArticle rawArticle)
        {
            rawArticleToMap = rawArticle;

            Article article = new Article()
            {
                Ref = rawArticleToMap.Ref,
                Libelle = rawArticleToMap.Libelle,
                Localisation = rawArticleToMap.Localisation,
                Quantite = rawArticleToMap.Quantite,
                InfosSupplementaires = rawArticleToMap.InfosSupplementaires
            };

            if (ArticleToMapIsSEL())
            {
                return new SEL(article);
            }
            else
            {
                // l'article à produire est du banalisé
                Banalise articleBanalise = new Banalise(article) { LienFlu = rawArticleToMap.LienFlu };

                if (ArticleToMapIsEPI())
                {
                    return new EPI(articleBanalise) { TypeEpi = rawArticleToMap.TypeEpi };
                }
                else
                {
                    return articleBanalise;
                }
            }
        }

        private bool ArticleToMapIsSEL()
        {
            return rawArticleToMap.IdMagasin == articlesSettings.GetIdMagasinSEL();
        }

        private bool ArticleToMapIsEPI()
        {
            return rawArticleToMap.TypeEpi != null;
        }

        private IArticlesSettings articlesSettings;
        private RawArticle rawArticleToMap;
    }
}
