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
                throw new ArgumentNullException(nameof(rawArticle));

            if (IsRawArticleBanalise(rawArticle))
                return rawArticle.IsEpi() ? CreateEpiFromRawArticle(rawArticle) : 
                    CreateBanaliseFromRawArticle(rawArticle);

            else if (IsRawArticleSEL(rawArticle))
                return CreateSelFromRawArticle(rawArticle);
            else
                throw new InvalidOperationException(nameof(rawArticle));
        }

        private bool IsRawArticleBanalise(RawArticle rawArticle)
        {
            return rawArticle.IdMagasin == settings.IdMagasinBanalise;
        }

        private Banalise CreateBanaliseFromRawArticle(RawArticle rawArticle)
        {
            return new Banalise(
                CreateArticleFromRawArticle(rawArticle), 
                rawArticle.LienFlu);
        }

        private Article CreateEpiFromRawArticle(RawArticle rawArticle)
        {
            return new Epi(
                CreateBanaliseFromRawArticle(rawArticle), 
                rawArticle.TypeEpi);
        }

        private bool IsRawArticleSEL(RawArticle rawArticle)
        {
            return rawArticle.IdMagasin == settings.IdMagasinSEL;
        }

        private Sel CreateSelFromRawArticle(RawArticle rawArticle)
        {
            return new Sel(
                CreateArticleFromRawArticle(rawArticle));
        }

        private Article CreateArticleFromRawArticle(RawArticle rawArticle)
        {
            return new Article(
                new ReferenceArticle(rawArticle.Ref),
                rawArticle.Libelle,
                rawArticle.Localisation,
                rawArticle.Quantite,
                rawArticle.InfosSupplementaires);
        }
    }
}
