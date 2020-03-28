using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface IAuthenticator
    {
        bool IsPasswordCorrect(UnhashedPassword passwordToTest);
    }
}
