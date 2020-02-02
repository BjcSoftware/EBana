using EBana.Excel.Core;
using EBana.Excel.Core.Exceptions;
using NSubstitute;
using NUnit.Framework;
using System.IO;

namespace EBana.Excel.UnitTest.Core
{
    [TestFixture]
    class ErrorHandlerExcelRecordReaderDecoratorTests
    {
        [Test]
        public void ReadAllRecordsFrom_IOException_Throws()
        {
            var decoratedReader = Substitute.For<IExcelRecordReader>();
            decoratedReader
                .When(r => r.ReadAllRecordsFrom(Arg.Any<ExcelSource>()))
                .Throw<IOException>();

            var errorHandler = new ErrorHandlerExcelRecordReaderDecorator(decoratedReader);

            Assert.Throws<FileOpenedByAnotherProcessException>(() =>
                errorHandler.ReadAllRecordsFrom(new ExcelSource("a path")));
        }

        [Test]
        public void ReadAllRecordsFrom_HeaderException_Throws()
        {
            var decoratedReader = Substitute.For<IExcelRecordReader>();
            decoratedReader
                .When(r => r.ReadAllRecordsFrom(Arg.Any<ExcelSource>()))
                .Throw<ExcelDataReader.Exceptions.HeaderException>();

            var errorHandler = new ErrorHandlerExcelRecordReaderDecorator(decoratedReader);

            Assert.Throws<NotAnExcelFileException>(() =>
                errorHandler.ReadAllRecordsFrom(new ExcelSource("a path")));
        }
    }
}
