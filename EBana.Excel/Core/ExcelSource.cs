using System;

namespace EBana.Excel.Core
{
    public class ExcelSource
    {
        public string FilePath { get; private set; }

        public ExcelSource(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            FilePath = filePath;
        }
    }
}
