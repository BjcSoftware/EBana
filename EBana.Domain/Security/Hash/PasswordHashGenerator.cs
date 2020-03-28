using EBana.Domain.Models;
using EBana.Domain.Security;

namespace EBana.Security.Hash
{
    public class PasswordHashGenerator : IPasswordHashGenerator
    {
        public string Generate(UnhashedPassword unhashedPassword)
        {
            int workFactor = 12;
            return
                BCrypt.Net.BCrypt.HashPassword(
                    unhashedPassword.Value, 
                    workFactor);
        }
    }
}
