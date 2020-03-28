using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBana.Domain.Models
{
    public class InvalidArticleReferenceException : InvalidOperationException
    {
        public InvalidArticleReferenceException()
        {
        }

        public InvalidArticleReferenceException(string message)
        : base(message)
        {
        }

        public InvalidArticleReferenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
