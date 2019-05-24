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
                throw new ArgumentNullException(nameof(credentialsUpdater));
            if (hash == null)
                throw new ArgumentNullException(nameof(hash));

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
            return new Credentials() { Password = hash.Hash(newPassword) };
        }
    }
}