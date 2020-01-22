using System;

namespace EBana.Excel.Core
{
    /// <summary>
    /// Représente un rectangle de cellules d'une feuille Excel.
    /// </summary>
    public class RectangularRange
    {
        private readonly ExcelCoords upperLeftCorner;
        private readonly ExcelCoords lowerRightCorner;

        public RectangularRange(ExcelCoords upperLeftCorner, ExcelCoords lowerRightCorner)
        {
            if (upperLeftCorner == null)
                throw new ArgumentNullException("upperLeftCorner");
            if (lowerRightCorner == null)
                throw new ArgumentNullException("lowerRightCorner");

            this.upperLeftCorner = upperLeftCorner;
            this.lowerRightCorner = lowerRightCorner;
        }

        public RectangularRange(ExcelCoords upperLeftCorner, uint width, uint height)
            : this(upperLeftCorner, 
                  new ExcelCoords(
                      upperLeftCorner.Column + width, 
                      upperLeftCorner.Row + height))
        {
        }

        public uint ColumnCount => lowerRightCorner.Column - upperLeftCorner.Column + 1;
        public uint RowCount => lowerRightCorner.Row - upperLeftCorner.Row + 1;
        public ExcelCoords UpperLeftCorner => upperLeftCorner;
    }
}
