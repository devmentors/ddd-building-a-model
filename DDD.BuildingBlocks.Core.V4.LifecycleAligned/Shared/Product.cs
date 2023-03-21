namespace DDD.BuildingBlocks.Core.V4.LifecycleAligned.Shared
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
    }
}
