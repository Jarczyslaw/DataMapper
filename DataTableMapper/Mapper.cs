using DataTableMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataTableMapper
{
    public class Mapper<TEntity> where TEntity : class, new()
    {
        private Lazy<Type> entityType = new Lazy<Type>(() => typeof(TEntity));
        public Type EntityType { get { return entityType.Value; } }

        public TEntity Map(DataRow row)
        {
            var entity = new TEntity();
            var lookup = CreateMappingLookup(row.Table);
            foreach (var mappingPair in lookup)
            {
                var fieldValue = row[mappingPair.ColumnName];
                MapperHelper.SetValue(entity, mappingPair.Property, fieldValue);
            }
            return entity;
        }

        public IEnumerable<TEntity> Map(DataTable table)
        {
            var entities = new List<TEntity>();
            var lookup = CreateMappingLookup(table);
            foreach (DataRow row in table.Rows)
            {
                var entity = new TEntity();
                foreach (var mappingPair in lookup)
                {
                    var fieldValue = row[mappingPair.ColumnName];
                    MapperHelper.SetValue(entity, mappingPair.Property, fieldValue);
                }
                entities.Add(entity);
            }
            return entities;
        }

        private List<MappingPair> CreateMappingLookup(DataTable table)
        {
            var lookup = new List<MappingPair>();
            var propsToMap = MapperHelper.GetPropertiesToMap(EntityType);
            foreach (var prop in propsToMap)
            {
                var mapping = prop.GetCustomAttribute<MappingAttribute>();
                if (FindMatchingMapping(mapping.Names, table.Columns, out string matchingMapping))
                {
                    var newPair = new MappingPair
                    {
                        Property = prop,
                        ColumnName = matchingMapping
                    };
                    lookup.Add(newPair);
                }
                else if (mapping.Required)
                    throw new MissingMappingNameException(string.Format("Matching mapping name for {0} not found", prop.Name));
            }
            return lookup;
        }

        private bool FindMatchingMapping(List<string> mappings, DataColumnCollection columns, out string matchingMapping)
        {
            matchingMapping = string.Empty;
            foreach (var mapping in mappings)
            {
                if (columns.Contains(mapping))
                {
                    matchingMapping = mapping;
                    return true;
                }
            }
            return false;
        }
    }
}
