using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper.Tests
{
    public class InvalidModelMapping : Model
    {
        [Mapping(false, "")]
        public string Test { get; set; }
    }
}
