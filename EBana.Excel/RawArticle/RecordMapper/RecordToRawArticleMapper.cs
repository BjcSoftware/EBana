using EBana.Domain.Models;
using EBana.Excel.Core;
using System;

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

        public RawArticle Map(Record record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            RawArticle rawArticle = new RawArticle()
            {
                Ref                     = record.GetFieldAt(fieldMapping.Ref).ToUpper(),
                Libelle                 = record.GetFieldAt(fieldMapping.Label),
                Localisation            = record.GetFieldAt(fieldMapping.Localisation),
                Quantite                = double.Parse(record.GetFieldAt(fieldMapping.Quantity)),
                IdMagasin               = int.Parse(record.GetFieldAt(fieldMapping.MagasinId)),
                LienFlu                 = record.GetFieldAt(fieldMapping.Flu),
                InfosSupplementaires    = record.GetFieldAt(fieldMapping.AdditionalInfos)
            };
            
            string epiLabel = record.GetFieldAt(fieldMapping.EpiLabel)?.Capitalize();
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
