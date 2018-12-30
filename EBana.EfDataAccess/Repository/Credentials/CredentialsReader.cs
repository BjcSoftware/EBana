using System.Linq;
using EBana.Domain.Security;
using EBana.Domain.Models;

namespace EBana.EfDataAccess.Repository
{
    public class CredentialsReader : ICredentialsReader
    {
        private readonly IReader<Credentials> credentialsReader;

        public CredentialsReader(IReader<Credentials> credentialsReader)
        {
            this.credentialsReader = credentialsReader;
        }

        public Credentials GetCredentials()
        {
            Credentials credentials = credentialsReader.GetAll().First();
            return credentials;
        }
    }
}
