namespace EBana.Security.Hash
{
    public interface IHash
    {
        string Hash(string textToHash);
        bool Verify(string plainText, string hashedText);
    }
}