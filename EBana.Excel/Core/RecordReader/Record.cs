using System;
using System.Collections.Generic;
using System.Linq;

namespace EBana.Excel.Core
{
    public class Record
    {
        private List<string> fields;

        public Record(List<string> fields)
        {
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            this.fields = fields;
        }

        public string GetFieldAt(int index)
        {
            return fields[index];
        }

        public bool IsEmpty()
        {
            return fields.All(f => string.IsNullOrEmpty(f));
        }
    }
}
