using DataMapper.Attributes;

namespace DataMapper.Tests.Export
{
    public class BoolToIntConverter : ExportConverterAttribute
    {
        public override object Convert(object value)
        {
            if (value is bool val)
            {
                return val ? 1 : 0;
            }
            return 0;
        }
    }
}