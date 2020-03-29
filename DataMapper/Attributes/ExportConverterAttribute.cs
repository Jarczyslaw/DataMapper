using System;

namespace DataMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ExportConverterAttribute : Attribute
    {
        public abstract object Convert(object value);
    }
}