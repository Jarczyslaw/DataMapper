using DataMapper.Attributes;

namespace DataMapper.Tests.Export
{
    public class ConverterEntity
    {
        [Export("IntValue", typeof(int))]
        [BoolToIntConverter]
        public bool BoolValue { get; set; }
    }
}