using System;
using System.Collections.Generic;

namespace EBana.Domain.Models
{
    public class TypeEpi
    {
        public string Value { get; private set; }

        private TypeEpi()
        { }

        public TypeEpi(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            Value = type.Capitalize();
        }

        public override bool Equals(object obj)
        {
            return obj is TypeEpi epi &&
                   Value == epi.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }

    public static class StringHelper
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            }
        }
    }
}
