using System;

namespace DataMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public Type ColumnType { get; set;  }

        public ExportAttribute(string columnName, Type columnType = null)
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }
    }
}