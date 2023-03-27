namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Shared
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public PaymentMethod PaymentMethod { get; private set; }
        
        public Payment(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }
    }

    public enum PaymentMethod
    {
        Cash,
        Cashless,
        Loan
    }
}
