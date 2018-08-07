using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableMapper.Exceptions
{
    public class InvalidMappingException : Exception
    {
        public InvalidMappingException(string message) 
            : base(message) { }
    }
}
