using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBana.Domain.Updater
{
    public class SimpleUpdateSourceValidator 
        : IUpdateSourceValidator
    {
        public bool IsValid(string updateSource)
        {
            throw new NotImplementedException();
        }
    }
}
