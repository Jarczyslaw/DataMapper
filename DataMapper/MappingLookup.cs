using DataMapper.Attributes;

namespace DataMapper
{
    internal class MappingLookup : PropertyColumnPair
    {
        public MappingConverterAttribute Converter { get; set; }
    }
}