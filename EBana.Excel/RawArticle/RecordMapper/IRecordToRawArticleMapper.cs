using System.Collections.Generic;

namespace EBana.Excel
{
    public interface IRecordToRawArticleMapper
    {
        /// <summary>
        /// Permet de créer un RawArticle à partir d'un Record.
        /// </summary>
        RawArticle Map(List<string> record);

        uint FieldCount { get; }
    }
}
