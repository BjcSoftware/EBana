using System.Collections.Generic;
using System;
using System.Linq;
using EBana.Excel.Core;

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
        private readonly IExcelFileFactory factory;

        public ExcelRawArticleProvider(
            IRecordToRawArticleMapper recordToRawArticleMapper,
            IExcelFileFactory factory)
        {
            if (recordToRawArticleMapper == null)
                throw new ArgumentNullException(nameof(recordToRawArticleMapper));
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            this.recordToRawArticleMapper = recordToRawArticleMapper;
            this.factory = factory;
        }

        public List<RawArticle> GetRawArticlesFrom(string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var xlFile = factory.CreateExcelFile(source);
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

        private string[,] ReadRawArticlesDataFrom(IExcelFileReader file, uint articleCount)
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
}
