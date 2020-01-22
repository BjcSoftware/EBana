using NUnit.Framework;

namespace EBana.Domain.Updater.UnitTests
{
    [TestFixture]
    class SimpleUpdateSourceValidatorTests
    {
        [Test]
        public void IsValid_NullSourcePassed_ReturnsFalse()
        {
            var validator = new SimpleUpdateSourceValidator();
            string nullSource = null;

            Assert.IsFalse(validator.IsValid(nullSource));
        }

        [Test]
        public void IsValid_EmptySourcePassed_ReturnsFalse()
        {
            var validator = new SimpleUpdateSourceValidator();
            string emptySource = string.Empty;

            Assert.IsFalse(validator.IsValid(emptySource));
        }

        [Test]
        public void IsValid_CorrectSourcePassed_ReturnsTrue()
        {
            var validator = new SimpleUpdateSourceValidator();
            string correctSource = "Correct Source";

            Assert.IsTrue(validator.IsValid(correctSource));
        }
    }
}
