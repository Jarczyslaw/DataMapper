using System;

namespace DataMapper.Exceptions
{
    public class InvalidMappingException : Exception
    {
        public InvalidMappingException(string message)
            : base(message) { }
    }
}