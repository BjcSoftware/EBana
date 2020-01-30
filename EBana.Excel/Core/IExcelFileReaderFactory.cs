namespace EBana.Excel.Core
{
    public interface IExcelFileReaderFactory
    {
        IExcelFileReader CreateExcelFile(string filePath);
    }
}
