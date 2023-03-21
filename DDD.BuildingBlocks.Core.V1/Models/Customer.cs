namespace DDD.BuildingBlocks.Core.V1.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
