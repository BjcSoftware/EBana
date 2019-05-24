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
                throw new ArgumentNullException(nameof(fieldMapping));

            this.fieldMapping = fieldMapping;
        }

        public uint FieldCount => fieldMapping.ColumnCount;

        public RawArticle Map(List<string> record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

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
            
            string epiLabel = record[fieldMapping.EpiLabel]?.Capitalize();
            if(epiLabel != null)
            {
                rawArticle.TypeEpi = new TypeEpi { Libelle = epiLabel };
            }

            return rawArticle;
        }
    }

    public static class StringHelper
    {
        public static string Capitalize(this string s)
        {
            if (s == string.Empty || s == null)
            {
                return s;
            }
            else
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            }
        }
    }
}
