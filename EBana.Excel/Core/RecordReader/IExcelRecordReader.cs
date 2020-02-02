using System.Collections.Generic;

namespace EBana.Excel.Core
{
    public interface IExcelRecordReader
    {
        IEnumerable<Record> ReadAllRecordsFrom(ExcelSource source);
    }
}
