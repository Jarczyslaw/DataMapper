using DataAccess;
using DataTableMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataTable = DataSource.GetTable();
            var mapper = new Mapper<Model>();
            var entities = mapper.Map(dataTable);
            foreach (var entity in entities)
                Console.WriteLine(entity);
            Console.ReadKey();
        }
    }
}
