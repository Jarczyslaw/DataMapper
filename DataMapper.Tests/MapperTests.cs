using DataMapper.DataAccess;
using DataMapper.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;

namespace DataMapper.Tests
{
    [TestClass]
    public class MapperTests
    {
        private Mapper<Model> mapper = new Mapper<Model>();

        [TestMethod]
        public void DataRowMapping()
        {
            var table = DataSource.GetTable();
            var row = table.Rows[0];
            var entity = mapper.Map(row);
            Assert.AreEqual(row[0], entity.Id);
            Assert.AreEqual(row[1], entity.Active);
            Assert.AreEqual(row[2], entity.Weight);
            Assert.AreEqual(row[3], entity.Cost);
            Assert.AreEqual(row[4], entity.Name);
            Assert.AreEqual(row[5], entity.AddedDate);
        }

        [TestMethod]
        public void DataTableMapping()
        {
            var table = DataSource.GetTable();
            var collection = mapper.Map(table);
            Assert.AreEqual(table.Rows.Count, collection.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidMappingException))]
        public void InvalidMapping()
        {
            var table = DataSource.GetTable();
            var collection = new Mapper<InvalidModelMapping>().Map(table);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMappingNameException))]
        public void MissingRequiredColumn()
        {
            var data = DataSource.GetTable();
            data.Columns.RemoveAt(0);
            var entities = mapper.Map(data);
        }

        [TestMethod]
        public void UseConverter()
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            var row = dt.NewRow();
            row["Value"] = 1;

            var mapper = new Mapper<ConverterEntity>();
            var entity = mapper.Map(row);
            Assert.AreEqual(1, entity.Value);
            Assert.AreEqual(true, entity.ConvertedValue);
        }
    }
}