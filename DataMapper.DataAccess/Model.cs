using DataMapper.Attributes;
using System;

namespace DataMapper.DataAccess
{
    public class Model
    {
        [Mapping("id", "Id")]
        public int Id { get; set; }

        [Mapping("active")]
        public bool Active { get; set; }

        [Mapping("weight")]
        public float Weight { get; set; }

        [Mapping("cost", "Cost", "_cost")]
        public decimal Cost { get; set; }

        [Mapping("name")]
        public string Name { get; set; }

        [Mapping("addedDate")]
        public DateTime AddedDate { get; set; }

        [Mapping(false, "orderId")]
        public int? OrderId { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Active: {1}, Weight: {2}, Cost: {3}, Name: {4}, AddedDate: {5}, OrderId: {6}",
                Id, Active, Weight, Cost, Name, AddedDate, OrderId.HasValue ? OrderId.Value.ToString() : "null");
        }
    }
}