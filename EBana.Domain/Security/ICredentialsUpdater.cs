using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface ICredentialsUpdater
    {
        void Update(Credentials newCredentials);
    }
}
