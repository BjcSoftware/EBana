using System;
using System.Collections.Generic;
using EBana.Excel.Core;
using NUnit.Framework;

namespace EBana.Excel.UnitTest.Core
{
    [TestFixture]
    class RecordTests
    {
        [Test]
        public void Constructor_NullFields_Throws()
        {
            List<string> nullFields = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new Record(nullFields));
        }

        [Test]
        public void IsEmpty_EmptyRecord_ReturnsTrue()
        {
            var emptyRecord = new Record(new List<string>());

            Assert.IsTrue(emptyRecord.IsEmpty());
        }

        [Test]
        public void IsEmpty_NotEmptyRecord_ReturnsFalse()
        {
            var notEmptyRecord = new Record(new List<string> { "a field" });

            Assert.IsFalse(notEmptyRecord.IsEmpty());
        }
    }
}
