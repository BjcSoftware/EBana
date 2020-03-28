using NUnit.Framework;
using System;

namespace EBana.Domain.Models.UnitTests
{
    [TestFixture]
    public class UnhashedPasswordTests
    {
        [TestCase(null)]
        public void Constructor_NullPassword_Throws(string password)
        {
            var exception = Assert.Catch<ArgumentNullException>(
                () => new UnhashedPassword(password));
        }
    }
}
