using DataMapper.DataAccess;

namespace DataMapper.Tests
{
    public class InvalidModelMapping : Model
    {
        [Mapping(false, "")]
        public string Test { get; set; }
    }
}