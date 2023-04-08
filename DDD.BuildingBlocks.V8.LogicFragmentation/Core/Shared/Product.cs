namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Shared
{
    //TODO: After V2 we lost encapsulation of this object???
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public bool LimitedAvailability { get; set; } //TODO: Why was it missing after V2?
    }
}
