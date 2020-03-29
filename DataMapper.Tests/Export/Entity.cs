using DataMapper.Attributes;
using System;

namespace DataMapper.Tests.Export
{
    public class Entity
    {
        [Export(nameof(IntValue))]
        public int IntValue { get; set; }

        [Export(nameof(BoolValue))]
        public bool BoolValue { get; set; }

        [Export(nameof(StringValue), typeof(string))]
        public string StringValue { get; set; }

        [Export(nameof(DateTimeValue))]
        public DateTime DateTimeValue { get; set; }
    }
}