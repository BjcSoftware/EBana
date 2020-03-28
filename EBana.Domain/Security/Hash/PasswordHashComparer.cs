using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public class PasswordHashComparer : IPasswordHashComparer
    {
        public bool AreMatching(
            HashedPassword hashedPassword, 
            UnhashedPassword unhashedPassword)
        {
            return BCrypt.Net.BCrypt
                .Verify(
                    unhashedPassword.Value, 
                    hashedPassword.Value);
        }
    }
}
