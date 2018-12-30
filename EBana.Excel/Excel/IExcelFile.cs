namespace EBana.Excel
{
    public interface IExcelFile
    {
        /// <summary>
        /// Récupérer les données contenues dans le recangle délimité par <paramref name="upperLhs"/> et <paramref name="lowerRhs"/>.
        /// </summary>
        /// <returns></returns>
        string[,] GetCellsAsStringInRange(ExcelCoords upperLhs, ExcelCoords lowerRhs);
        int RowCount();
    }
}
