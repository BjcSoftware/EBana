namespace EBana.Excel.Core
{
    public interface IExcelFileReader
    {
        /// <summary>
        /// Récupérer les données contenues dans le recangle.
        /// </summary>
        string[,] GetCellsAsStringInRange(RectangularRange range);
        
        /// <summary>
        /// Nombre de lignes, on considère la fin du fichier à la première ligne où la première colonne est vide.
        /// </summary>
        uint RowCount { get; }
    }
}
