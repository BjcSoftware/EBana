using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface ICredentialsReader
    {
        Credentials GetCredentials();
    }
}
