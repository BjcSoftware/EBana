using System.Collections.Generic;
using System.Linq;

namespace EBana.Domain.Models
{
    public class ReferenceArticle
    {
        public string Value { get; private set; }

        private static List<string> ReferencePrefixes = new List<string>() { "N", "Z", "X", "I" };
        private static int ReferenceLength = 8;

        private ReferenceArticle()
        { }

        public ReferenceArticle(string referenceArticle)
        {
            if (InvalidReference(referenceArticle))
            {
                throw new InvalidArticleReferenceException();
            }

            Value = referenceArticle.ToUpper();
        }

        private bool InvalidReference(string reference)
        {
            return
                IncorrectReferencePrefix(reference) ||
                reference.Length != ReferenceLength;
        }

        private bool IncorrectReferencePrefix(string reference)
        {
            return !ReferencePrefixes.Contains(reference.ToUpper().First().ToString());
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ReferenceArticle article &&
                   Value == article.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}
