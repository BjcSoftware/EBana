namespace EBana.Excel.Core
{
    public interface IExcelFileFactory
    {
        IExcelFileReader CreateExcelFile(string filePath);
    }
}
