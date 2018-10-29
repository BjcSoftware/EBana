using EBana.Models;

namespace Data.Repository
{
    public interface ICredentialsReader
    {
        Credentials GetCredentials();
    }
}
