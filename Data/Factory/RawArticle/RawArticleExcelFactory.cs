using System.Collections.Generic;
using Excel;
using EBana.Models;

namespace EBana.Factory
{
    public class RawArticleExcelFactory : IRawArticleFactory
    {
        public RawArticleExcelFactory(string excelFilePath)
        {
            var xlFile = new ExcelFile(excelFilePath);
            articleCount = xlFile.RowCount() - 1;
            rawArticleData = ReadRawArticleDataFrom(xlFile, articleCount);

            labelToTypeEpi = new Dictionary<string, TypeEpi>();
        }

        private string[,] ReadRawArticleDataFrom(ExcelFile file, int articleCount)
        {
            ExcelCoordinates upperLhs = new ExcelCoordinates(1, 2);

            int xOffset = upperLhs.Column + articleFieldCount - 1;
            int yOffset = upperLhs.Row + articleCount - 1;
            ExcelCoordinates lowerRhs = new ExcelCoordinates(
                upperLhs.Column + xOffset,
                upperLhs.Row + yOffset);

            string[,] articleRawData = file.GetCellsAsStringInRange(upperLhs, lowerRhs);

            return articleRawData;
        }

        public List<RawArticle> CreateAllRawArticles()
        {
            List<RawArticle> articles = new List<RawArticle>();

            for(int i = 0; i < articleCount; i++)
            {
                articles.Add(CreateNextRawArticle());
            }

            return articles;
        }

        private RawArticle CreateNextRawArticle()
        {
            RawArticle rawArticle = new RawArticle()
            {
                Ref = rawArticleData[currentArticleIndex, refColumn].ToUpper(),
                Libelle = rawArticleData[currentArticleIndex, libelleColumn],
                Localisation = rawArticleData[currentArticleIndex, localisationColumn],
                Quantite = double.Parse(rawArticleData[currentArticleIndex, quantiteColumn]),
                IdMagasin = int.Parse(rawArticleData[currentArticleIndex, idMagasinColumn]),
                LienFlu = rawArticleData[currentArticleIndex, fluColumn],
                InfosSupplementaires = rawArticleData[currentArticleIndex, infosSupplementairesColumn]
            };

            /// le type d'EPI est géré à part car plus complexe à gérer <see cref="LabelToTypeEpi"/>
            string epiType = rawArticleData[currentArticleIndex, typeEpiColumn]?.Capitalize();

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

        private const int refColumn = 0;
        private const int libelleColumn = 1;
        private const int divisionColumn = 2;
        private const int idMagasinColumn = 3;
        private const int localisationColumn = 4;
        private const int quantiteColumn = 5;
        private const int fluColumn = 6;
        private const int infosSupplementairesColumn = 7;
        private const int epiIdentifierColumn = 8;
        private const int typeEpiColumn = 9;

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
