using System;

namespace EBana.Excel.Core.Exceptions
{
    public class FileOpenedByAnotherProcessException : InvalidOperationException
    {
        public FileOpenedByAnotherProcessException()
        {
        }

        public FileOpenedByAnotherProcessException(string message)
        : base(message)
        {
        }

        public FileOpenedByAnotherProcessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
