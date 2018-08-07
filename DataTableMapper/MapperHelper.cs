using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableMapper
{
    public static class MapperHelper
    {
        public static IEnumerable<PropertyInfo> GetPropertiesToMap(Type type)
        {
            return type.GetProperties()
                .Where(p => p.IsDefined(typeof(MappingAttribute), false));
        }

        public static void SetValue(object entity, PropertyInfo property, object value)
        {
            if (value == DBNull.Value)
                value = null;
            property.SetValue(entity, value);
        }

        public static bool IsNullable(Type type)
        {
            if (!type.IsValueType)
                return true;
            if (Nullable.GetUnderlyingType(type) != null)
                return true;
            return false;
        }
    }
}
