using DataMapper.Attributes;
using DataMapper.DataAccess;

namespace DataMapper.Tests.Mapping
{
    public class InvalidModelMapping : Model
    {
        [Mapping(false, "")]
        public string Test { get; set; }
    }
}