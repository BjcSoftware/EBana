using NUnit.Framework;
using System;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    public class ExcelFileTests
    {
        [Test]
        public void Constructor_NullPathPassed_Throws()
        {
            string nullPath = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelFile(nullPath));
        }
    }
}
