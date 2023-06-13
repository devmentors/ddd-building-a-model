using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared
{
    public record Shipment
    {
        public string City { get;}
        public string StreetName { get;}
        public int StreetNumber { get; }
        public string ReceiverFullName { get; }

        private Shipment()
        {
            
        }
        
        public Shipment(string city, string streetName, int streetNumber, string receiverFullName)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new InvalidShipmentException();
            }

            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new InvalidShipmentException();
            }

            if (string.IsNullOrWhiteSpace(receiverFullName))
            {
                throw new InvalidShipmentException();
            }

            City = city;
            StreetName = streetName;
            StreetNumber = streetNumber;
            ReceiverFullName = receiverFullName;
        }
    }
}
