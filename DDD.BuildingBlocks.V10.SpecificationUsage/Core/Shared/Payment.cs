namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public PaymentMethod PaymentMethod { get; private set; }

        private Payment()
        {
        }
        
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
