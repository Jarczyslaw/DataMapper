using DataMapper.Attributes;
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
            var lookup = CreateMappingLookup(Helpers.GetColumnNames(row.Table));
            return MapEntity(columnName => row[columnName], lookup);
        }

        public IEnumerable<TEntity> Map(DataTable table)
        {
            var entities = new List<TEntity>();
            var lookup = CreateMappingLookup(Helpers.GetColumnNames(table));
            foreach (DataRow row in table.Rows)
            {
                var entity = MapEntity(columnName => row[columnName], lookup);
                entities.Add(entity);
            }
            return entities;
        }

        public IEnumerable<TEntity> Map(IDataReader reader)
        {
            var entities = new List<TEntity>();
            var lookup = CreateMappingLookup(Helpers.GetColumnNames(reader));
            while (reader.Read())
            {
                var entity = MapEntity(columnName => reader[columnName], lookup);
                entities.Add(entity);
            }
            return entities;
        }

        private TEntity MapEntity(Func<string, object> valueFunc, List<MappingPair> lookup)
        {
            var entity = new TEntity();
            foreach (var mappingPair in lookup)
            {
                var fieldValue = valueFunc(mappingPair.ColumnName);
                if (mappingPair.Converter != null)
                {
                    fieldValue = mappingPair.Converter.Convert(fieldValue);
                }
                Helpers.SetValue(entity, mappingPair.Property, fieldValue);
            }
            return entity;
        }

        private List<MappingPair> CreateMappingLookup(List<string> columns)
        {
            var lookup = new List<MappingPair>();
            foreach (var prop in Helpers.GetPropertiesToMap(EntityType))
            {
                var mapping = prop.GetCustomAttribute<MappingAttribute>();
                if (FindMatchingMapping(mapping.Names, columns, out string matchingMapping))
                {
                    lookup.Add(new MappingPair
                    {
                        Property = prop,
                        ColumnName = matchingMapping,
                        Converter = prop.GetCustomAttribute<ConverterAttribute>()
                    });
                }
                else if (mapping.Required)
                {
                    throw new MissingMappingNameException(string.Format("Matching mapping name for {0} not found", prop.Name));
                }
            }
            return lookup;
        }

        private bool FindMatchingMapping(List<string> mappingNames, List<string> columns, out string matchingMapping)
        {
            matchingMapping = string.Empty;
            foreach (var mapping in mappingNames)
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