using DataMapper.DataAccess;
using System;

namespace DataMapper.TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataTable = DataSource.GetTable();
            var mapper = new Mapper<Model>();
            foreach (var entity in mapper.Map(dataTable))
                Console.WriteLine(entity);
            Console.ReadKey();
        }
    }
}