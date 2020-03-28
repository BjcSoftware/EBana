using EBana.Domain.Security;
using System;

namespace EBana.Domain.Models
{
    public class HashedPassword
    {
        public string Value { get; private set; }

        private HashedPassword()
        { }

        public HashedPassword(
            UnhashedPassword passwordToHash, 
            IPasswordHashGenerator hashGenerator)
        {
            if (passwordToHash == null)
                throw new ArgumentNullException(nameof(passwordToHash));
            if (hashGenerator == null)
                throw new ArgumentNullException(nameof(hashGenerator));

            Value = hashGenerator.Generate(passwordToHash);
        }

        public bool MatchWith(
            UnhashedPassword unhashedPassword, 
            IPasswordHashComparer hashComparer)
        {
            if (unhashedPassword == null)
                throw new ArgumentNullException(nameof(unhashedPassword));
            if (hashComparer == null)
                throw new ArgumentNullException(nameof(hashComparer));

            return hashComparer.AreMatching(this, unhashedPassword);
        }
    }
}
