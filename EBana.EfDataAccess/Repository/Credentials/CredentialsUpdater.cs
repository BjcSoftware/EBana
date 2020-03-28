using EBana.Domain.Security;
using EBana.Domain.Models;
using System;

namespace EBana.EfDataAccess.Repository
{
    public class CredentialsUpdater : ICredentialsUpdater
    {
        private readonly IWriter<Credentials> credentialsWriter;
        private readonly ICredentialsReader credentialsReader;

        public CredentialsUpdater(
            IWriter<Credentials> credentialsWriter, 
            ICredentialsReader credentialsReader)
        {
            if (credentialsWriter == null)
                throw new ArgumentNullException(nameof(credentialsWriter));
            if (credentialsReader == null)
                throw new ArgumentNullException(nameof(credentialsReader));

            this.credentialsWriter = credentialsWriter;
            this.credentialsReader = credentialsReader;
        }

        public void Update(Credentials newCredentials)
        {
            if (newCredentials == null)
                throw new ArgumentNullException("newCredentials");

            RemoveCurrentCredentials();
            credentialsWriter.Add(newCredentials);
            credentialsWriter.Save();
        }

        private void RemoveCurrentCredentials()
        {
            Credentials currentCredentials = credentialsReader.GetCredentials();
            credentialsWriter.Remove(currentCredentials.Id);
        }
    }
}
