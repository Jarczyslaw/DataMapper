using DataMapper.Attributes;
using System;

namespace DataMapper
{
    internal class ExportLookup : PropertyColumnPair
    {
        public Type ColumnType { get; set; }
        public ExportConverterAttribute Converter { get; set; }
    }
}