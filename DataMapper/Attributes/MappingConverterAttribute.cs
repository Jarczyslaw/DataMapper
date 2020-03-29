using System;

namespace DataMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MappingConverterAttribute : Attribute
    {
        public abstract object Convert(object value);
    }
}