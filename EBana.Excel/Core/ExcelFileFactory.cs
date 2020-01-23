namespace EBana.Excel.Core
{
    public class ExcelFileFactory : IExcelFileFactory
    {
        public IExcelFileReader CreateExcelFile(string filePath)
        {
            return new ExcelFileReader(filePath);
        }
    }
}
