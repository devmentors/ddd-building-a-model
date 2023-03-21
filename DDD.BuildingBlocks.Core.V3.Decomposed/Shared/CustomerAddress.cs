namespace DDD.BuildingBlocks.Core.V3.Decomposed.Shared
{
    public class CustomerAddress
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public int StreetNumber { get; private set; }
        
        public CustomerAddress(string city, string streetName, int streetNumber)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException($"'{nameof(city)}' cannot be null or whitespace.", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentException($"'{nameof(streetName)}' cannot be null or whitespace.", nameof(streetName));
            }

            City = city;
            StreetName = streetName;
            StreetNumber = streetNumber;
        }
    }
}
