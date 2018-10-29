using System;

namespace Excel
{
    public class ExcelCoordinates
    {
        public ExcelCoordinates(int column, int row)
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
