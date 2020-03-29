using DataMapper.Attributes;

namespace DataMapper.Tests.Mapping
{
    public class IntToBoolConverterAttribute : MappingConverterAttribute
    {
        public override object Convert(object value)
        {
            if (value is int val)
            {
                return val != 0;
            }
            return false;
        }
    }
}