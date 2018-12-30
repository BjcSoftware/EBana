using EBana.Security.Hash;
using System;

namespace EBana.Domain.Security
{
    public class Authenticator : IAuthenticator
    {
        private readonly ICredentialsReader credentialsReader;
        private readonly IHash hash;

        public Authenticator(
            ICredentialsReader credentialsReader, 
            IHash hash)
        {
            if (credentialsReader == null)
                throw new ArgumentNullException("credentialsReader");
            if (hash == null)
                throw new ArgumentNullException("hash");

            this.credentialsReader = credentialsReader;
            this.hash = hash;
        }

        public bool IsPasswordCorrect(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var hashedPassword = GetPasswordHash();
            return hash.Verify(password, hashedPassword);
        }

        private string GetPasswordHash()
        {
            return credentialsReader.GetCredentials().Password;
        }
    }
}