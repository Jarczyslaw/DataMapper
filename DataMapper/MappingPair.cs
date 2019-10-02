using System.Reflection;

namespace DataMapper
{
    public class MappingPair
    {
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
    }
}