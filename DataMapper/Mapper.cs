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

        public DataTable ExportToDataTable(IEnumerable<TEntity> entities)
        {
            var result = new DataTable();
            var lookup = CreateExportLookup();
            foreach (var look in lookup)
            {
                result.Columns.Add(look.ColumnName, look.ColumnType);
            }
            foreach (var entity in entities)
            {
                var newRow = ExportEntity(result, entity, lookup);
                result.Rows.Add(newRow);
            }
            return result;
        }

        private TEntity MapEntity(Func<string, object> valueFunc, List<MappingLookup> lookup)
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

        private DataRow ExportEntity(DataTable dataTable, TEntity entity, List<ExportLookup> lookup)
        {
            var row = dataTable.NewRow();
            foreach (var look in lookup)
            {
                var fieldValue = look.Property.GetValue(entity);
                if (look.Converter != null)
                {
                    fieldValue = look.Converter.Convert(fieldValue);
                }
                row[look.ColumnName] = fieldValue ?? DBNull.Value;
            }
            return row;
        }

        private List<MappingLookup> CreateMappingLookup(List<string> columns)
        {
            var lookup = new List<MappingLookup>();
            foreach (var prop in Helpers.GetProperties<MappingAttribute>(EntityType))
            {
                var mapping = prop.GetCustomAttribute<MappingAttribute>();
                if (FindMatchingMapping(mapping.Names, columns, out string matchingMapping))
                {
                    lookup.Add(new MappingLookup
                    {
                        Property = prop,
                        ColumnName = matchingMapping,
                        Converter = prop.GetCustomAttribute<MappingConverterAttribute>()
                    });
                }
                else if (mapping.Required)
                {
                    throw new MissingMappingNameException(string.Format("Matching mapping name for {0} not found", prop.Name));
                }
            }
            return lookup;
        }

        private List<ExportLookup> CreateExportLookup()
        {
            var lookup = new List<ExportLookup>();
            foreach (var prop in Helpers.GetProperties<ExportAttribute>(EntityType))
            {
                var propAttr = prop.GetCustomAttribute<ExportAttribute>();
                lookup.Add(new ExportLookup
                {
                    ColumnName = propAttr.ColumnName,
                    ColumnType = propAttr.ColumnType ?? Helpers.ExtractTypeFromNullable(prop.PropertyType),
                    Property = prop,
                    Converter = prop.GetCustomAttribute<ExportConverterAttribute>()
                });
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