using EBana.Domain.Security;
using NSubstitute;
using NUnit.Framework;
using System;

namespace EBana.Domain.Models.UnitTests
{
    [TestFixture]
    public class HashedPasswordTests
    {
        [Test]
        public void Constructor_NullPassword_Throws()
        {
            var stubHashGenerator = Substitute.For<IPasswordHashGenerator>();
            UnhashedPassword nullPassword = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => new HashedPassword(nullPassword, stubHashGenerator));
        }

        [Test]
        public void Constructor_NullHashGenerator_Throws()
        {
            IPasswordHashGenerator nullHashGenerator = null;

            var exception = Assert.Throws<ArgumentNullException>(
                () => new HashedPassword(
                    CreateStubUnhashedPassword(), 
                    nullHashGenerator));
        }

        [Test]
        public void Value_PasswordIsHashedUsingHashGenerator()
        {
            var stubHashGenerator = Substitute.For<IPasswordHashGenerator>();
            stubHashGenerator
                .Generate(Arg.Any<UnhashedPassword>())
                .Returns("HashedPassword");

            var hashedPassword = new HashedPassword(
                CreateStubUnhashedPassword(), 
                stubHashGenerator);

            Assert.AreEqual(
                "HashedPassword",
                hashedPassword.Value);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void MatchWith_ReturnsCorrectResult(bool areMatching)
        {
            HashedPassword hashedPassword = CreateHashedPassword();

            Assert.AreEqual(
                areMatching,
                hashedPassword.MatchWith(
                    new UnhashedPassword("password"),
                    CreateStubPasswordHashComparerReturning(areMatching)));
        }

        private HashedPassword CreateHashedPassword()
        {
            var stubHashingFunction = Substitute.For<IPasswordHashGenerator>();
            stubHashingFunction
                .Generate(Arg.Any<UnhashedPassword>())
                .Returns("hash");

            return new HashedPassword(
                new UnhashedPassword("password"),
                stubHashingFunction);
        }

        private IPasswordHashComparer CreateStubPasswordHashComparerReturning(bool areMatching)
        {
            var stubHashComparer = Substitute.For<IPasswordHashComparer>();
            stubHashComparer
                .AreMatching(Arg.Any<HashedPassword>(), Arg.Any<UnhashedPassword>())
                .Returns(areMatching);

            return stubHashComparer;
        }

        public UnhashedPassword CreateStubUnhashedPassword()
        {
            return new UnhashedPassword("password");
        }
    }
}
