using System;

namespace EBana.Excel.Core.Exceptions
{
    public class NotAnExcelFileException
        : InvalidOperationException
    {
        public NotAnExcelFileException()
        {
        }

        public NotAnExcelFileException(string message)
        : base(message)
        {
        }

        public NotAnExcelFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
