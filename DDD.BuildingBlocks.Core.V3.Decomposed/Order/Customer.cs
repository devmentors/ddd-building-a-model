namespace DDD.BuildingBlocks.Core.V3.Decomposed.Order
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
