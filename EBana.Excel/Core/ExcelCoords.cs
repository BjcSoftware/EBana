using System;

namespace EBana.Excel.Core
{
    /// <summary>
    /// Représente les coordonnées d'une cellule d'une feuille Excel.
    /// </summary>
    public class ExcelCoords
    {
        public ExcelCoords(uint column, uint row)
        {
            if (column < 1)
                throw new ArgumentOutOfRangeException("column");
            if (row < 1)
                throw new ArgumentOutOfRangeException("row");

            Column = column;
            Row = row;
        }

        public uint Column { get; set; }
        public uint Row { get; set; }
    }
}
