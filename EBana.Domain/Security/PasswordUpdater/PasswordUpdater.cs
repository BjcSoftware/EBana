using EBana.Domain.Models;
using EBana.Security.Hash;
using System;

namespace EBana.Domain.Security
{
    public class PasswordUpdater : IPasswordUpdater
    {
        private readonly ICredentialsUpdater credentialsUpdater;
        private readonly IHash hash;

        public PasswordUpdater(
            ICredentialsUpdater credentialsUpdater, 
            IHash hash)
        {
            if (credentialsUpdater == null)
                throw new ArgumentNullException("credentialsUpdater");
            if (hash == null)
                throw new ArgumentNullException("hash");

            this.credentialsUpdater = credentialsUpdater;
            this.hash = hash;
        }

        public void Update(string newPassword)
        {
            Credentials newCredentials = CreateNewCredentialsFromPassword(newPassword);
            credentialsUpdater.Update(newCredentials);
        }

        private Credentials CreateNewCredentialsFromPassword(string newPassword)
        {
            string newPasswordHash = hash.Hash(newPassword);
            var newCredentials = new Credentials() { Password = newPasswordHash };
            return newCredentials;
        }
    }
}