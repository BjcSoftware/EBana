using EBana.Domain.Models;
using System;
using System.Collections.Generic;

namespace EBana.Excel
{
    /// <summary>
    /// Permet de transformer une ligne de données en un RawArticle.
    /// </summary>  
    public class RecordToRawArticleMapper : IRecordToRawArticleMapper
    {
        private readonly ArticleFieldToRecordFieldMapping fieldMapping;

        public RecordToRawArticleMapper(ArticleFieldToRecordFieldMapping fieldMapping)
        {
            if (fieldMapping == null)
                throw new ArgumentNullException("fieldMapping");

            this.fieldMapping = fieldMapping;
        }

        public uint FieldCount => fieldMapping.ColumnCount;

        public RawArticle Map(List<string> record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            RawArticle rawArticle = new RawArticle()
            {
                Ref                     = record[fieldMapping.Ref].ToUpper(),
                Libelle                 = record[fieldMapping.Label],
                Localisation            = record[fieldMapping.Localisation],
                Quantite                = double.Parse(record[fieldMapping.Quantity]),
                IdMagasin               = int.Parse(record[fieldMapping.MagasinId]),
                LienFlu                 = record[fieldMapping.Flu],
                InfosSupplementaires    = record[fieldMapping.AdditionalInfos]
            };
            
            string epiType = record[fieldMapping.EpiType]?.Capitalize();
            if(epiType != null)
            {
                rawArticle.TypeEpi = new TypeEpi { Libelle = epiType };
            }

            return rawArticle;
        }
    }
}
