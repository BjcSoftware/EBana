using System;
using EBana.Excel;
using NUnit.Framework;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    class ExcelCoordsTests
    {
        [Test]
        [TestCase(0, 2)]
        [TestCase(-1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, -1)]
        public void Constructor_CoordLessThanOne_Throws(int column, int row)
        {
            var exception = Assert.Catch<ArgumentException>(
                () => new ExcelCoords(column, row));
        }
    }
}
