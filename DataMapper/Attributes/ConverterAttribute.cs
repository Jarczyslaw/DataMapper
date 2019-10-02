using System;

namespace DataMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ConverterAttribute : Attribute
    {
        public abstract object Convert(object value);
    }
}