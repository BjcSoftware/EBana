using EBana.Domain.Security;
using EBana.Domain.Models;

namespace EBana.EfDataAccess.Repository
{
    public class CredentialsUpdater : ICredentialsUpdater
    {
        private readonly IWriter<Credentials> credentialsWriter;
        private readonly ICredentialsReader credentialsReader;

        public CredentialsUpdater(IWriter<Credentials> credentialsWriter, ICredentialsReader credentialsReader)
        {
            this.credentialsWriter = credentialsWriter;
            this.credentialsReader = credentialsReader;
        }

        public void Update(Credentials newCredentials)
        {
            RemoveCurrentCredentials();
            credentialsWriter.Add(newCredentials);
            credentialsWriter.Save();
        }

        private void RemoveCurrentCredentials()
        {
            Credentials currentCredentials = credentialsReader.GetCredentials();
            credentialsWriter.Remove(currentCredentials.IdCredentials);
        }
    }
}
