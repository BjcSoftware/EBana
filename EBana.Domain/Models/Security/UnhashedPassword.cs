using System;

namespace EBana.Domain.Models
{
    public class UnhashedPassword
    {
        public string Value { get; private set; }

        public UnhashedPassword(string password)
        {
            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            Value = password;
        }
    }
}
