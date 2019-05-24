namespace EBana.Security.Hash
{
    public class BCryptHash : IHash
    {
        public string Hash(string textToHash)
        {
            int salt = 12;
            return BCrypt.Net.BCrypt.HashPassword(textToHash, salt);
        }

        public bool Verify(string plainText, string hashedText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hashedText);
        }
    }
}
