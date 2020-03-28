using EBana.Domain.Models;
using EBana.Domain.Security.Event;
using System;

namespace EBana.Domain.Security
{
    public class PasswordUpdater : IPasswordUpdater
    {
        private readonly ICredentialsUpdater credentialsUpdater;
        private readonly IPasswordHashGenerator passwordHashGenerator;
        private readonly IEventHandler<PasswordUpdated> handler;

        public PasswordUpdater(
            ICredentialsUpdater credentialsUpdater,
            IPasswordHashGenerator passwordHashGenerator,
            IEventHandler<PasswordUpdated> handler)
        {
            if (credentialsUpdater == null)
                throw new ArgumentNullException(nameof(credentialsUpdater));
            if (passwordHashGenerator == null)
                throw new ArgumentNullException(nameof(passwordHashGenerator));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            this.credentialsUpdater = credentialsUpdater;
            this.passwordHashGenerator = passwordHashGenerator;
            this.handler = handler;
        }

        public void Update(UnhashedPassword newPassword)
        {
            credentialsUpdater.Update(
                CreateCredentialsFromPassword(newPassword));
            handler.Handle(new PasswordUpdated());
        }

        private Credentials CreateCredentialsFromPassword(UnhashedPassword newPassword)
        {
            return 
                new Credentials(
                    new HashedPassword(
                        newPassword, passwordHashGenerator));
        }
    }
}