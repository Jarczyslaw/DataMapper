using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataMapper
{
    public class Mapper<TEntity> where TEntity : class, new()
    {
        private readonly Lazy<Type> entityType = new Lazy<Type>(() => typeof(TEntity));
        public Type EntityType => entityType.Value;

        public TEntity Map(DataRow row)
        {
            var lookup = CreateMappingLookup(row.Table);
            return MapEntity(row, lookup);
        }

        public IEnumerable<TEntity> Map(DataTable table)
        {
            var entities = new List<TEntity>();
            var lookup = CreateMappingLookup(table);
            foreach (DataRow row in table.Rows)
            {
                var entity = MapEntity(row, lookup);
                entities.Add(entity);
            }
            return entities;
        }

        private TEntity MapEntity(DataRow row, List<MappingPair> lookup)
        {
            var entity = new TEntity();
            foreach (var mappingPair in lookup)
            {
                var fieldValue = row[mappingPair.ColumnName];
                MapperHelper.SetValue(entity, mappingPair.Property, fieldValue);
            }
            return entity;
        }

        private List<MappingPair> CreateMappingLookup(DataTable table)
        {
            var lookup = new List<MappingPair>();
            foreach (var prop in MapperHelper.GetPropertiesToMap(EntityType))
            {
                var mapping = prop.GetCustomAttribute<MappingAttribute>();
                if (FindMatchingMapping(mapping.Names, table.Columns, out string matchingMapping))
                {
                    lookup.Add(new MappingPair
                    {
                        Property = prop,
                        ColumnName = matchingMapping
                    });
                }
                else if (mapping.Required)
                {
                    throw new MissingMappingNameException(string.Format("Matching mapping name for {0} not found", prop.Name));
                }
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