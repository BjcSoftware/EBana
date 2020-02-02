using EBana.Excel.Core;

namespace EBana.Excel
{
    public interface IRecordToRawArticleMapper
    {
        /// <summary>
        /// Permet de créer un RawArticle à partir d'un Record.
        /// </summary>
        RawArticle Map(Record record);
    }
}
