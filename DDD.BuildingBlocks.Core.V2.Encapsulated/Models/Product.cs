namespace DDD.BuildingBlocks.Core.V2.Encapsulated.Models
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        public string SKU { get; private set; }
        public decimal Price { get; private set; }
        public bool LimitedAvailability { get; }
        
        public Product(string name, string sku, decimal price, bool limitedAvailability, Guid? id = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SKU = sku ?? throw new ArgumentNullException(nameof(sku));
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price = price;
            LimitedAvailability = limitedAvailability;
            if (id is not null)
            {
                Id = id.Value;
            }
        }
    }
}
