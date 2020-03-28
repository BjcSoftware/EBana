using EBana.Domain.Models;
using System;

namespace EBana.Domain.Security
{
    public class Authenticator : IAuthenticator
    {
        private readonly ICredentialsReader credentialsReader;
        private readonly IPasswordHashComparer passwordHashComparer;

        public Authenticator(
            ICredentialsReader credentialsReader,
            IPasswordHashComparer passwordHashComparer)
        {
            if (credentialsReader == null)
                throw new ArgumentNullException(nameof(credentialsReader));
            if (passwordHashComparer == null)
                throw new ArgumentNullException(nameof(passwordHashComparer));

            this.credentialsReader = credentialsReader;
            this.passwordHashComparer = passwordHashComparer;
        }

        public bool IsPasswordCorrect(UnhashedPassword passwordToTest)
        {
            if (passwordToTest == null)
                throw new ArgumentNullException(nameof(passwordToTest));

            return ActualPassword()
                .MatchWith(passwordToTest, passwordHashComparer);
        }

        private HashedPassword ActualPassword()
        {
            return credentialsReader.GetCredentials().Password;
        }
    }
}