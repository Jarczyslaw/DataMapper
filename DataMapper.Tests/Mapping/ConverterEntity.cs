using DataMapper.Attributes;

namespace DataMapper.Tests.Mapping
{
    public class ConverterEntity
    {
        [Mapping("Value", Required = true)]
        public int Value { get; set; }

        [Mapping("Value", Required = true)]
        [IntToBoolConverter]
        public bool ConvertedValue { get; set; }
    }
}