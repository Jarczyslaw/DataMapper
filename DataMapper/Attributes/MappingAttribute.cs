using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MappingAttribute : Attribute
    {
        public bool Required { get; set; }
        public List<string> Names { get; set; }

        public MappingAttribute(params string[] names)
            : this(true, names) { }

        public MappingAttribute(bool required, params string[] names)
        {
            Validate(names);

            Required = required;
            Names = names.ToList();
        }

        private void Validate(string[] names)
        {
            if (names == null || names.Length == 0)
            {
                throw new InvalidMappingException("Mapping names array can not be empty");
            }

            if (names.Any(m => string.IsNullOrEmpty(m)))
            {
                throw new InvalidMappingException("Mapping name can not be empty");
            }
        }
    }
}