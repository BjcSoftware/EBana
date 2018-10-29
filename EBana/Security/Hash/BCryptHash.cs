namespace EBana.Security.Hash
{
    public class BCryptHash : IHash
    {
        public string Hash(string textToHash)
        {
            int salt = 12;
            string hashedText = BCrypt.Net.BCrypt.HashPassword(textToHash, salt);
            return hashedText;
        }

        public bool Verify(string plainText, string hashedText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hashedText);
        }
    }
}
