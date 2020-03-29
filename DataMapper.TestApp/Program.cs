using DataMapper.DataAccess;
using System;
using System.Data;

namespace DataMapper.TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataTable = DataSource.GetTable();

            Console.WriteLine("Mapped entities:");
            var mapper = new Mapper<Model>();
            var entities = mapper.Map(dataTable);
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }

            Console.WriteLine();
            Console.WriteLine("Exported data table:");
            var newDataTable = mapper.ExportToDataTable(entities);
            foreach (DataRow row in newDataTable.Rows)
            {
                foreach (DataColumn column in newDataTable.Columns)
                {
                    Console.Write($"{column.ColumnName}: {row[column.ColumnName]} ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}