using System;
using Xl = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace EBana.Excel
{
	public class ExcelFile : IExcelFile
	{
		# region Constructeur / Destructeur
		public ExcelFile(string excelFilePath)
		{
            if (excelFilePath == null)
                throw new ArgumentNullException("excelFilePath");

			mXlApp = new Xl.Application();
			mXlWorkbook = mXlApp.Workbooks.Open(excelFilePath);

            var format = mXlWorkbook.FileFormat;

			mXlWorkSheet = (Xl.Worksheet)mXlWorkbook.Sheets[1];
			mXlRange = mXlWorkSheet.UsedRange;
		}
		
		~ExcelFile()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			
			Marshal.ReleaseComObject(mXlRange);
			Marshal.ReleaseComObject(mXlWorkSheet);
			
			mXlApp.Quit();
			Marshal.ReleaseComObject(mXlApp);
		}
		
		# endregion
		
		# region Méthodes

        public string[,] GetCellsAsStringInRange(
            ExcelCoords upperLhs, 
            ExcelCoords lowerRhs)
        {
            object[,] range = GetRawCellsInRange(upperLhs, lowerRhs);

            int rowCount = range.GetLength(0);
            int columnCount = range.GetLength(1);
            string[,] strRange = new string[rowCount, columnCount];

            for (int col = 0; col < columnCount; ++col)
            {
                for (int row = 0; row < rowCount; ++row)
                {
                    strRange[row, col] = range[row+1, col+1]?.ToString();
                }
            }

            return strRange;
        }

        private object[,] GetRawCellsInRange(
            ExcelCoords upperLhs, 
            ExcelCoords lowerRhs)
        {
            Xl.Range upperLhsCell = mXlWorkSheet.Cells[upperLhs.Row, upperLhs.Column];
            Xl.Range lowerRhsCell = mXlWorkSheet.Cells[lowerRhs.Row, lowerRhs.Column];

            Xl.Range range = mXlWorkSheet.get_Range(upperLhsCell, lowerRhsCell);
            return (object[,])range.get_Value(Xl.XlRangeValueDataType.xlRangeValueDefault);
        }

        public int RowCount()
		{
            int lastUsedRow = mXlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                            Xl.XlSearchOrder.xlByRows, Xl.XlSearchDirection.xlPrevious,
                            false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            return lastUsedRow;
        }
		
		# endregion
		
		# region Membres privés
		private Xl.Application mXlApp;
		private Xl.Workbook mXlWorkbook;
		private Xl.Worksheet mXlWorkSheet;
		private Xl.Range mXlRange;
		# endregion
	}
}
