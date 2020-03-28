using EBana.Domain.Models;
using EBana.Domain.Security.Event;
using EBana.Security.Hash;
using System;

namespace EBana.Domain.Security
{
    public class PasswordUpdater : IPasswordUpdater
    {
        private readonly ICredentialsUpdater credentialsUpdater;
        private readonly IHash hash;
        private readonly IEventHandler<PasswordUpdated> handler;

        public PasswordUpdater(
            ICredentialsUpdater credentialsUpdater, 
            IHash hash,
            IEventHandler<PasswordUpdated> handler)
        {
            if (credentialsUpdater == null)
                throw new ArgumentNullException(nameof(credentialsUpdater));
            if (hash == null)
                throw new ArgumentNullException(nameof(hash));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            this.credentialsUpdater = credentialsUpdater;
            this.hash = hash;
            this.handler = handler;
        }

        public void Update(string newPassword)
        {
            Credentials newCredentials = CreateNewCredentialsFromPassword(newPassword);
            credentialsUpdater.Update(newCredentials);
            handler.Handle(new PasswordUpdated());
        }

        private Credentials CreateNewCredentialsFromPassword(string newPassword)
        {
            return new Credentials(hash.Hash(newPassword));
        }
    }
}