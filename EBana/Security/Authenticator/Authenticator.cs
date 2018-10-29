using Data.Repository;
using EBana.Security.Hash;

namespace EBana.Security
{
    public class Authenticator
    {
        private readonly ICredentialsReader credentialsReader;
        private readonly IHash hash;

        public Authenticator(ICredentialsReader credentialsReader, IHash hash)
        {
            this.credentialsReader = credentialsReader;
            this.hash = hash;
        }

        public bool IsPasswordCorrect(string password)
        {
            var hashedPassword = GetPasswordHash();
            return hash.Verify(password, hashedPassword);
        }

        private string GetPasswordHash()
        {
            return credentialsReader.GetCredentials().Password;
        }
    }
}