using DataMapper.Attributes;

namespace DataMapper.Tests
{
    public class IntToBoolConverterAttribute : ConverterAttribute
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