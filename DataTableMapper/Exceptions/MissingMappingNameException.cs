using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableMapper.Exceptions
{
    public class MissingMappingNameException : Exception
    {
        public MissingMappingNameException(string name)
            : base(name) { }
    }
}
