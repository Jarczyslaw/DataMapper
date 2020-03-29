using DataMapper.Attributes;
using System;

namespace DataMapper.DataAccess
{
    public class Model
    {
        [Mapping("id", "Id")]
        [Export(nameof(Id))]
        public int Id { get; set; }

        [Mapping("active")]
        [Export(nameof(Active))]
        public bool Active { get; set; }

        [Mapping("weight")]
        [Export(nameof(Weight))]
        public float Weight { get; set; }

        [Mapping("cost", "Cost", "_cost")]
        [Export(nameof(Cost))]
        public decimal Cost { get; set; }

        [Mapping("name")]
        [Export(nameof(Name))]
        public string Name { get; set; }

        [Mapping("addedDate")]
        [Export(nameof(AddedDate))]
        public DateTime AddedDate { get; set; }

        [Mapping(false, "orderId")]
        [Export(nameof(OrderId))]
        public int? OrderId { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Active: {1}, Weight: {2}, Cost: {3}, Name: {4}, AddedDate: {5}, OrderId: {6}",
                Id, Active, Weight, Cost, Name, AddedDate, OrderId.HasValue ? OrderId.Value.ToString() : "null");
        }
    }
}