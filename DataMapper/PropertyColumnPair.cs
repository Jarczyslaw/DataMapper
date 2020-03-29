using System.Reflection;

namespace DataMapper
{
    internal class PropertyColumnPair
    {
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
    }
}