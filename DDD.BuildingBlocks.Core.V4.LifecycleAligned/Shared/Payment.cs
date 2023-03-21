namespace DDD.BuildingBlocks.Core.V4.LifecycleAligned.Shared
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
