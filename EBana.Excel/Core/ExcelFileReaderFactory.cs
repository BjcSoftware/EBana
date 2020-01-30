namespace EBana.Excel.Core
{
    public class ExcelFileReaderFactory : IExcelFileReaderFactory
    {
        public IExcelFileReader CreateExcelFile(string filePath)
        {
            return new ExcelFileReader(filePath);
        }
    }
}
