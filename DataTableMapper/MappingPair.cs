using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataTableMapper
{
    public class MappingPair
    {
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
    }
}
