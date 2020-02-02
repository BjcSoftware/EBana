using System.Collections.Generic;
using System;
using System.Linq;
using EBana.Excel.Core;

namespace EBana.Excel
{
    /// <summary>
    /// Permet d'extraire des RawArticles d'un fichier Excel.
    /// </summary>
    public class ExcelRawArticleProvider : IRawArticleProvider
    {
        private readonly IRecordToRawArticleMapper recordToRawArticleMapper;
        private readonly IExcelRecordReader recordReader;

        public ExcelRawArticleProvider(
            IRecordToRawArticleMapper recordToRawArticleMapper,
            IExcelRecordReader recordReader)
        {
            if (recordToRawArticleMapper == null)
                throw new ArgumentNullException(nameof(recordToRawArticleMapper));
            if (recordReader == null)
                throw new ArgumentNullException(nameof(recordReader));

            this.recordToRawArticleMapper = recordToRawArticleMapper;
            this.recordReader = recordReader;
        }

        public IEnumerable<RawArticle> GetRawArticlesFrom(string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return 
                recordReader
                    .ReadAllRecordsFrom(new ExcelSource(source))
                    .Select(record => recordToRawArticleMapper.Map(record));
        }
    }
}
