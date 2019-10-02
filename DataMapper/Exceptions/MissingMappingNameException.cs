using System;

namespace DataMapper.Exceptions
{
    public class MissingMappingNameException : Exception
    {
        public MissingMappingNameException(string name)
            : base(name) { }
    }
}