using System;

namespace EBana.Domain.Updater.Exceptions
{
    public class InvalidUpdateSourceException : Exception
    {
        public InvalidUpdateSourceException()
        {
        }

        public InvalidUpdateSourceException(string message)
            : base(message)
        {
        }

        public InvalidUpdateSourceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
