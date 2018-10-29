using Data.Repository;
using EBana.Models;
using NUnit.Framework;
using NSubstitute;
using EBana.Security.Hash;
using EBana.Security;

namespace EBana.UnitTests.Security
{
    [TestFixture]
    class AuthenticatorTests
    {
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

        private ICredentialsReader CreateStubCredentialsReader()
        {
            var credentialsReader = Substitute.For<ICredentialsReader>();
            credentialsReader.GetCredentials()
                .Returns(new Credentials());

            return credentialsReader;
        }
    }
}
