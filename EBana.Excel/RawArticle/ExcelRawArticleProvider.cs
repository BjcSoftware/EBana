using System.Collections.Generic;
using EBana.Domain.Models;
using System;

namespace EBana.Excel
{
    /// <summary>
    /// Permet d'extraire des RawArticles d'un fichier Excel.
    /// La feuille excel doit contenir un article par ligne.
    /// </summary>
    public class ExcelRawArticleProvider : IRawArticleProvider
    {
        private readonly ArticleFieldToColumnMapping columnMapping;
        private readonly IExcelFileFactory excelFileFactory;

        public ExcelRawArticleProvider(
            ArticleFieldToColumnMapping columnMapping,
            IExcelFileFactory excelFileFactory)
        {
            if (columnMapping == null)
                throw new ArgumentNullException("columnMapping");
            if (excelFileFactory == null)
                throw new ArgumentNullException("excelFileFactory");

            this.columnMapping = columnMapping;
            this.excelFileFactory = excelFileFactory;
        }

        public List<RawArticle> GetRawArticles(string source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IExcelFile xlFile = excelFileFactory.CreateExcelFile(source);
            articleCount = xlFile.RowCount() - 1;
            rawArticleData = ReadRawArticlesDataFrom(xlFile, articleCount);

            labelToTypeEpi = new Dictionary<string, TypeEpi>();

            List<RawArticle> articles = new List<RawArticle>();
            currentArticleIndex = 0;
            for (int i = 0; i < articleCount; i++)
            {
                articles.Add(GetNextRawArticle());
            }

            return articles;
        }

        private string[,] ReadRawArticlesDataFrom(IExcelFile file, int articleCount)
        {
            ExcelCoords upperLhs = new ExcelCoords(1, 2);

            int xOffset =  articleFieldCount - 1;
            int yOffset = articleCount - 1;
            ExcelCoords lowerRhs = new ExcelCoords(
                upperLhs.Column + xOffset,
                upperLhs.Row + yOffset);

            string[,] articleRawData = file.GetCellsAsStringInRange(upperLhs, lowerRhs);

            return articleRawData;
        }

        private RawArticle GetNextRawArticle()
        {
            RawArticle rawArticle = new RawArticle()
            {
                Ref = rawArticleData[currentArticleIndex, columnMapping.Ref].ToUpper(),
                Libelle = rawArticleData[currentArticleIndex, columnMapping.Label],
                Localisation = rawArticleData[currentArticleIndex, columnMapping.Localisation],
                Quantite = double.Parse(rawArticleData[currentArticleIndex, columnMapping.Quantity]),
                IdMagasin = int.Parse(rawArticleData[currentArticleIndex, columnMapping.MagasinId]),
                LienFlu = rawArticleData[currentArticleIndex, columnMapping.Flu],
                InfosSupplementaires = rawArticleData[currentArticleIndex, columnMapping.AdditionalInfos]
            };

            /// le type d'EPI est géré à part car plus complexe à gérer <see cref="LabelToTypeEpi"/>
            string epiType = rawArticleData[currentArticleIndex, columnMapping.EpiType]?.Capitalize();

            // si l'article courant est un EPI -> réaliser le traitement associé
            if (epiType != null)
            {
                if (!labelToTypeEpi.ContainsKey(epiType))
                {
                    labelToTypeEpi.Add(epiType, new TypeEpi() { Libelle = epiType });
                }

                /// récupérer une instance commune de TypeEpi <see cref="LabelToTypeEpi"/>
                rawArticle.TypeEpi = labelToTypeEpi[epiType];
            }

            currentArticleIndex++;

            return rawArticle;
        }

        #region membres privés
        private int articleCount;
        private string[,] rawArticleData;

        private int currentArticleIndex;

        // les EPIs du même type doivent contenir la même instance de TypeEpi
        // sinon la table TypeEpi de la DB sera remplie de doublons (une occurrence par EPI du type donné)
        private Dictionary<string, TypeEpi> labelToTypeEpi;

        private const int articleFieldCount = 10;

        #endregion
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
