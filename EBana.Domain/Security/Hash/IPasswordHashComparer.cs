using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface IPasswordHashComparer
    {
        bool AreMatching(
            HashedPassword hashedPassword, 
            UnhashedPassword unhashedPassword);
    }
}
