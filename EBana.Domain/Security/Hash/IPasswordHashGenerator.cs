using EBana.Domain.Models;

namespace EBana.Domain.Security
{
    public interface IPasswordHashGenerator
    {
        string Generate(UnhashedPassword unhashedPassword);
    }
}
