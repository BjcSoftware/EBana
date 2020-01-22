namespace EBana.Excel.Core
{
    public interface IExcelFileFactory
    {
        IExcelFile CreateExcelFile(string filePath);
    }
}
