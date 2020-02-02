namespace EBana.Excel.Core
{
    public class ExcelRecordReaderParams
    {
        public int FieldPerRecordCount { get; private set; }

        public int LineNumberWhereReadingStarts { get; private set; }

        public ExcelRecordReaderParams(int fieldPerRecordCount, int lineNumberWhereReadingStarts)
        {
            FieldPerRecordCount = fieldPerRecordCount;
            LineNumberWhereReadingStarts = lineNumberWhereReadingStarts;
        }
    }
}
