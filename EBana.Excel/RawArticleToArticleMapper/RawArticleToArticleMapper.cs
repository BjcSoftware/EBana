using EBana.Domain;
using EBana.Domain.Models;
using System;

namespace EBana.Excel
{
    /// <summary>
    /// Permet de créer un Article à partir d'un RawArticle.
    /// </summary>
    public class RawArticleToArticleMapper : IRawArticleToArticleMapper
    {
        private readonly IArticleSettings articleSettings;

        public RawArticleToArticleMapper(IArticleSettings articleSettings)
        {
            if (articleSettings == null)
                throw new ArgumentNullException("articleSettings");

            this.articleSettings = articleSettings;
        }

        public Article Map(RawArticle rawArticle)
        {
            if (rawArticle == null)
                throw new ArgumentNullException("rawArticle");

            Article article = new Article()
            {
                Ref = rawArticle.Ref,
                Libelle = rawArticle.Libelle,
                Localisation = rawArticle.Localisation,
                Quantite = rawArticle.Quantite,
                InfosSupplementaires = rawArticle.InfosSupplementaires
            };

            if (IsRawArticleSEL(rawArticle))
            {
                return new SEL(article);
            }
            else
            {
                // l'article à produire est du banalisé
                Banalise articleBanalise = new Banalise(article) { LienFlu = rawArticle.LienFlu };

                if (IsRawArticleEPI(rawArticle))
                {
                    return new EPI(articleBanalise) { TypeEpi = rawArticle.TypeEpi };
                }
                else
                {
                    return articleBanalise;
                }
            }
        }

        private bool IsRawArticleSEL(RawArticle rawArticle)
        {
            return rawArticle.IdMagasin == articleSettings.IdMagasinSEL;
        }

        private bool IsRawArticleEPI(RawArticle rawArticle)
        {
            return rawArticle.TypeEpi != null;
        }
    }
}
