using EBana.Models;

namespace Data.Repository
{
    public interface ICredentialsUpdater
    {
        void Update(Credentials newCredentials);
    }
}
