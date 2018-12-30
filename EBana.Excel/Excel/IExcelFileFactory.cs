namespace EBana.Excel
{
    public interface IExcelFileFactory
    {
        IExcelFile CreateExcelFile(string filePath);
    }
}
