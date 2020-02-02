using EBana.Excel.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace EBana.Excel.Core
{
    public class ErrorHandlerExcelRecordReaderDecorator : IExcelRecordReader
    {
        private readonly IExcelRecordReader decoratedReader;

        public ErrorHandlerExcelRecordReaderDecorator(IExcelRecordReader decoratedReader)
        {
            if (decoratedReader == null)
                throw new ArgumentNullException(nameof(decoratedReader));

            this.decoratedReader = decoratedReader;
        }

        public IEnumerable<Record> ReadAllRecordsFrom(ExcelSource source)
        {
            try
            {
                return decoratedReader.ReadAllRecordsFrom(source);
            }
            catch (IOException)
            {
                throw new FileOpenedByAnotherProcessException();
            }
            catch (ExcelDataReader.Exceptions.HeaderException)
            {
                throw new NotAnExcelFileException();
            }
        }
    }
}
