using System;
using System.Data;

namespace DataMapper.DataAccess
{
    public static class DataSource
    {
        public static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("active", typeof(bool));
            table.Columns.Add("weight", typeof(float));
            table.Columns.Add("_cost", typeof(decimal));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("addedDate", typeof(DateTime));
            table.Columns.Add("orderId", typeof(int));

            table.Rows.Add(new object[] { 1, true, 12f, 10m, "Computer", DateTime.Now.AddDays(-4), null });
            table.Rows.Add(new object[] { 2, true, 3f, 20m, "Smartphone", DateTime.Now.AddDays(-3), 10 });
            table.Rows.Add(new object[] { 3, false, 456f, 30m, "Headphones", DateTime.Now.AddDays(-2), DBNull.Value });
            table.Rows.Add(new object[] { 4, true, 78f, 40m, "Bicycle", DateTime.Now.AddDays(-1), 42 });

            return table;
        }
    }
}