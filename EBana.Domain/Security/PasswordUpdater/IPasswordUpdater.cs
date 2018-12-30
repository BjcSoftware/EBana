namespace EBana.Domain.Security
{
    public interface IPasswordUpdater
    {
        void Update(string newPassword);
    }
}
