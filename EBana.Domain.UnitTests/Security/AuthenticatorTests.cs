using EBana.Domain.Models;
using NUnit.Framework;
using NSubstitute;
using EBana.Security.Hash;
using System;

namespace EBana.Domain.Security.UnitTests
{
    [TestFixture]
    class AuthenticatorTests
    {
        [Test]
        public void Constructor_NullCredentialsReaderPassed_Throws()
        {
            ICredentialsReader nullCredentialsReader = null;
            var stubHash = Substitute.For<IHash>();

            var exception = Assert.Catch<ArgumentNullException>(
                () => new Authenticator(
                    nullCredentialsReader,
                    stubHash));
        }

        [Test]
        public void Constructor_NullHashPassed_Throws()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            IHash nullHash = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new Authenticator(
                    stubCredentialsReader,
                    nullHash));
        }

        [Test]
        public void IsPasswordCorrect_NullPassword_Throws()
        {
            var authenticator = CreateAuthenticator();
            string nullPassword = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => authenticator.IsPasswordCorrect(nullPassword));
        }

        [Test]
        public void IsPasswordCorrect_CorrectPassword_ReturnsTrue()
        {
            var stubHash = Substitute.For<IHash>();
            stubHash
                .Verify(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);
            var authenticator = CreateAuthenticator(stubHash);

            bool isPasswordCorrect = authenticator.IsPasswordCorrect("a correct password");

            Assert.True(isPasswordCorrect);
        }

        [Test]
        public void IsPasswordCorrect_IncorrectPassword_ReturnsFalse()
        {
            var stubHash = Substitute.For<IHash>();
            stubHash
                .Verify(Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            var authenticator = CreateAuthenticator(stubHash);

            bool isPasswordCorrect = authenticator.IsPasswordCorrect("an incorrect password");

            Assert.False(isPasswordCorrect);
        }

        private Authenticator CreateAuthenticator(IHash hash)
        {
            ICredentialsReader stubCredentialsReader = CreateStubCredentialsReader();
            var authenticator = new Authenticator(stubCredentialsReader, hash);

            return authenticator;
        }

        private Authenticator CreateAuthenticator()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            var stubHash = Substitute.For<IHash>();

            return new Authenticator(
                stubCredentialsReader,
                stubHash);
        }

        private ICredentialsReader CreateStubCredentialsReader()
        {
            var stubCredentialsReader = Substitute.For<ICredentialsReader>();
            stubCredentialsReader.GetCredentials()
                .Returns(new Credentials("stubPassword"));

            return stubCredentialsReader;
        }
    }
}
