using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface IPasswordUpdater
    {
        void Update(UnhashedPassword newPassword);
    }
}
