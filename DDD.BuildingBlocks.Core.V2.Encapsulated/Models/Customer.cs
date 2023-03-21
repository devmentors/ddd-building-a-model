namespace DDD.BuildingBlocks.Core.V2.Encapsulated.Models
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        

        public Customer(string name, string address, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException($"'{nameof(address)}' cannot be null or whitespace.", nameof(address));
            }

            Name = name;

            if (id is not null)
            {
                Id = id.Value;
            }
        }
    }
}
