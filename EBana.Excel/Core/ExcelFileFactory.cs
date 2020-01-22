namespace EBana.Excel.Core
{
    public class ExcelFileFactory : IExcelFileFactory
    {
        public IExcelFile CreateExcelFile(string filePath)
        {
            return new ExcelFile(filePath);
        }
    }
}
