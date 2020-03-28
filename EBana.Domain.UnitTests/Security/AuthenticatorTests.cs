using EBana.Domain.Models;
using NUnit.Framework;
using NSubstitute;
using System;
using EBana.Security.Hash;

namespace EBana.Domain.Security.UnitTests
{
    [TestFixture]
    class AuthenticatorTests
    {
        [Test]
        public void Constructor_NullCredentialsReaderPassed_Throws()
        {
            ICredentialsReader nullCredentialsReader = null;
            var stubHash = Substitute.For<IPasswordHashComparer>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new Authenticator(
                    nullCredentialsReader,
                    stubHash));
        }

        [Test]
        public void Constructor_NullPasswordHashComparerPassed_Throws()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            IPasswordHashComparer nullPasswordHashComparer = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new Authenticator(
                    stubCredentialsReader,
                    nullPasswordHashComparer));
        }

        [Test]
        public void IsPasswordCorrect_NullPassword_Throws()
        {
            var authenticator = CreateAuthenticator();
            UnhashedPassword nullPassword = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => authenticator.IsPasswordCorrect(nullPassword));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void IsPasswordCorrect_ReturnsCorrectResult(bool expected)
        {
            var authenticator = CreateAuthenticator(
                CreateStubPasswordHashComparerReturning(expected));

            Assert.AreEqual(
                expected,
                authenticator
                    .IsPasswordCorrect(
                        new UnhashedPassword("password")));
        }

        private IPasswordHashComparer CreateStubPasswordHashComparerReturning(bool areMatching)
        {
            var stubPasswordHashComparer = Substitute.For<IPasswordHashComparer>();
            stubPasswordHashComparer
                .AreMatching(Arg.Any<HashedPassword>(), Arg.Any<UnhashedPassword>())
                .Returns(areMatching);

            return stubPasswordHashComparer;
        }

        private Authenticator CreateAuthenticator(IPasswordHashComparer passwordHashComparer)
        {
            var authenticator = new Authenticator(
                CreateStubCredentialsReader(),
                passwordHashComparer);

            return authenticator;
        }

        private Authenticator CreateAuthenticator()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            var stubPasswordHashComparer = Substitute.For<IPasswordHashComparer>();

            return new Authenticator(
                stubCredentialsReader,
                stubPasswordHashComparer);
        }

        private ICredentialsReader CreateStubCredentialsReader()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            stubCredentialsReader
                .GetCredentials()
                .Returns(
                    new Credentials(
                        new HashedPassword(
                            new UnhashedPassword("stubPassword"), 
                            new PasswordHashGenerator())));

            return stubCredentialsReader;
        }
    }
}
