using Data.Repository;
using EBana.Models;
using EBana.Security.Hash;

namespace EBana.Security
{
    public class PasswordUpdater
    {
        private readonly ICredentialsUpdater credentialsUpdater;
        private readonly IHash hash;

        public PasswordUpdater(ICredentialsUpdater credentialsUpdater, IHash hash)
        {
            this.credentialsUpdater = credentialsUpdater;
            this.hash = hash;
        }

        public void Update(string newPassword)
        {
            Credentials newCredentials = CreateNewCredentials(newPassword);
            credentialsUpdater.Update(newCredentials);
        }

        private Credentials CreateNewCredentials(string newPassword)
        {
            string newPasswordHash = hash.Hash(newPassword);
            var newCredentials = new Credentials() { Password = newPasswordHash };
            return newCredentials;
        }
    }
}