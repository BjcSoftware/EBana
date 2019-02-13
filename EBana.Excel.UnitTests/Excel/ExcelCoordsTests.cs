using System;
using NUnit.Framework;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    class ExcelCoordsTests
    {
        [Test]
        [TestCase((uint)0, (uint)2)]
        [TestCase((uint)2, (uint)0)]
        [TestCase((uint)0, (uint)0)]
        public void Constructor_CoordLessThanOne_Throws(uint column, uint row)
        {
            var exception = Assert.Catch<ArgumentException>(
                () => new ExcelCoords(column, row));
        }
    }
}
