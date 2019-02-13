using System.Collections.Generic;
using System;
using System.Linq;

namespace EBana.Excel
{
    /// <summary>
    /// Permet d'extraire des RawArticles d'un fichier Excel.
    /// La feuille Excel doit contenir un article par ligne.
    /// </summary>
    public class ExcelRawArticleProvider : IRawArticleProvider
    {
        private uint articleCount;
        private string[,] rawArticleData;
        private uint currentArticleIndex;

        private readonly IRecordToRawArticleMapper recordToRawArticleMapper;
        private readonly IExcelFileFactory excelFileFactory;

        public ExcelRawArticleProvider(
            IRecordToRawArticleMapper recordToRawArticleMapper,
            IExcelFileFactory excelFileFactory)
        {
            if (recordToRawArticleMapper == null)
                throw new ArgumentNullException("recordToRawArticleMapper");
            if (excelFileFactory == null)
                throw new ArgumentNullException("excelFileFactory");

            this.recordToRawArticleMapper = recordToRawArticleMapper;
            this.excelFileFactory = excelFileFactory;
        }

        public List<RawArticle> GetRawArticlesFrom(string source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var xlFile = excelFileFactory.CreateExcelFile(source);
            articleCount = xlFile.RowCount - 1;
            rawArticleData = ReadRawArticlesDataFrom(xlFile, articleCount);

            List<RawArticle> articles = new List<RawArticle>();
            currentArticleIndex = 0;
            for (int i = 0; i < articleCount; i++)
            {
                articles.Add(GetNextRawArticle());
            }

            return articles;
        }

        private string[,] ReadRawArticlesDataFrom(IExcelFile file, uint articleCount)
        {
            ExcelCoords upperLhs = new ExcelCoords(1, 2);
            uint xOffset =  recordToRawArticleMapper.FieldCount - 1;
            uint yOffset = articleCount - 1;

            return file.GetCellsAsStringInRange(
                new RectangularRange(upperLhs, xOffset, yOffset));
        }

        private RawArticle GetNextRawArticle()
        {
            var record = rawArticleData.SliceRow(currentArticleIndex++).ToList();
            return recordToRawArticleMapper.Map(record);
        }
    }

    public static class ArrayHelper
    {
        /// <summary>
        /// Permet d'extraire une ligne d'un tableau à 2 dimensions.
        /// </summary>
        public static IEnumerable<T> SliceRow<T>(this T[,] array, uint row)
        {
            for (var i = array.GetLowerBound(1); i <= array.GetUpperBound(1); i++)
            {
                yield return array[row, i];
            }
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
