using System;

namespace EBana.Excel
{
    /// <summary>
    /// Représente les coordonnées d'une cellule d'une feuille Excel.
    /// </summary>
    public class ExcelCoords
    {
        public ExcelCoords(int column, int row)
        {
            if(column < 1 || row < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            Column = column;
            Row = row;
        }

        public int Column { get; set; }
        public int Row { get; set; }     
    }
}
