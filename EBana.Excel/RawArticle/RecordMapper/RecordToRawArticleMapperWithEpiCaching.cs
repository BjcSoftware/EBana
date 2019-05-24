using EBana.Domain.Models;
using System;
using System.Collections.Generic;

namespace EBana.Excel
{
    /// <summary>
    /// Decorator pour IRecordToRawArticleMapper.
    /// A pour rôle de faire en sorte que les RawArticle possédant un TypeEpi
    /// possèdent la même instance de TypeEpi si leurs TypeEpi sont égaux (au sens de TypeEpi.Equals).
    /// 
    /// Ceci est nécessaire pour éviter que l'EntityFramework crée des TypeEpi en double dans la DB.
    /// </summary>
    public class RecordToRawArticleMapperWithEpiCaching : IRecordToRawArticleMapper
    {
        private readonly IRecordToRawArticleMapper decoratedMapper;
        private List<TypeEpi> cachedEpiType;

        public RecordToRawArticleMapperWithEpiCaching(IRecordToRawArticleMapper decoratedMapper)
        {
            if (decoratedMapper == null)
              throw new ArgumentNullException(nameof(decoratedMapper));

            this.decoratedMapper = decoratedMapper;
            cachedEpiType = new List<TypeEpi>();
        }

        public uint FieldCount => decoratedMapper.FieldCount;

        public RawArticle Map(List<string> record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            RawArticle article = decoratedMapper.Map(record);
            article.TypeEpi = GetEpiTypeFor(article);

            return article;
        }

        private TypeEpi GetEpiTypeFor(RawArticle article)
        {
            return article.IsEpi() ? GetUniqueEpiTypeFor(article) : null;
        }

        private TypeEpi GetUniqueEpiTypeFor(RawArticle rawArticle)
        {
            if (!cachedEpiType.Contains(rawArticle.TypeEpi))
            {
                cachedEpiType.Add(rawArticle.TypeEpi);
            }
            
            return cachedEpiType
                .Find(e => e.Equals(rawArticle.TypeEpi));
        }
    }
}
