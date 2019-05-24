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
        private readonly IArticleSettings settings;

        public RawArticleToArticleMapper(IArticleSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            this.settings = settings;
        }

        public Article Map(RawArticle rawArticle)
        {
            if (rawArticle == null)
                throw new ArgumentNullException("rawArticle");

            if (IsRawArticleBanalise(rawArticle))
                return rawArticle.IsEpi() ? CreateEpiFromRawArticle(rawArticle) : 
                    CreateBanaliseFromRawArticle(rawArticle);

            else if (IsRawArticleSEL(rawArticle))
                return CreateSelFromRawArticle(rawArticle);
            else
                throw new InvalidOperationException("rawArticle");
        }

        private bool IsRawArticleBanalise(RawArticle rawArticle)
        {
            return rawArticle.IdMagasin == settings.IdMagasinBanalise;
        }

        private Banalise CreateBanaliseFromRawArticle(RawArticle rawArticle)
        {
            return new Banalise(
                CreateArticleFromRawArticle(rawArticle)) { LienFlu = rawArticle.LienFlu };
        }

        private Article CreateEpiFromRawArticle(RawArticle rawArticle)
        {
            return new EPI(
                CreateBanaliseFromRawArticle(rawArticle)) { TypeEpi = rawArticle.TypeEpi };
        }

        private bool IsRawArticleSEL(RawArticle rawArticle)
        {
            return rawArticle.IdMagasin == settings.IdMagasinSEL;
        }

        private SEL CreateSelFromRawArticle(RawArticle rawArticle)
        {
            return new SEL(
                CreateArticleFromRawArticle(rawArticle));
        }

        private Article CreateArticleFromRawArticle(RawArticle rawArticle)
        {
            return new Article()
            {
                Ref = rawArticle.Ref,
                Libelle = rawArticle.Libelle,
                Localisation = rawArticle.Localisation,
                Quantite = rawArticle.Quantite,
                InfosSupplementaires = rawArticle.InfosSupplementaires
            };
        }
    }
}
