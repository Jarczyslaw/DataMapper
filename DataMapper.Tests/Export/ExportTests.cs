using System;
using System.Collections.Generic;
using System.Reflection;
using DataMapper.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMapper.Tests.Export
{
    [TestClass]
    public class ExportTests
    {
        [TestMethod]
        public void Export()
        {
            var entity = new Entity
            {
                BoolValue = true,
                DateTimeValue = DateTime.Now,
                IntValue = 10,
                StringValue = "test"
            };
            var dataTable = new Mapper<Entity>()
                .ExportToDataTable(new List<Entity> { entity });
            foreach (var prop in Helpers.GetProperties<ExportAttribute>(entity.GetType()))
            {
                var attr = prop.GetCustomAttribute<ExportAttribute>();
                Assert.IsTrue(dataTable.Columns.Contains(attr.ColumnName));
            }
            Assert.AreEqual(1, dataTable.Rows.Count);
        }

        [TestMethod]
        public void ExportWithConverter()
        {
            var entity = new ConverterEntity
            {
                BoolValue = true
            };
            var dataTable = new Mapper<ConverterEntity>()
                .ExportToDataTable(new List<ConverterEntity> { entity });

            Assert.AreEqual(typeof(int), dataTable.Columns[0].DataType);
            Assert.AreEqual(1, dataTable.Rows.Count);
            Assert.AreEqual(1, dataTable.Rows[0][0]);
        }
    }
}
