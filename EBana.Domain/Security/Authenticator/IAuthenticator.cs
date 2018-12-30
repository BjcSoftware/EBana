namespace EBana.Domain.Security
{
    public interface IAuthenticator
    {
        bool IsPasswordCorrect(string password);
    }
}
