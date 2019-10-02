using DataMapper.Attributes;
using System.Reflection;

namespace DataMapper
{
    internal class MappingPair
    {
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
        public ConverterAttribute Converter { get; set; }
    }
}