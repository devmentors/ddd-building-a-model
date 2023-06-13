namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public PaymentMethod PaymentMethod { get; private set; }
        public PayerType PayerType { get; private set; }

        private Payment()
        {
        }
        
        public Payment(PaymentMethod paymentMethod, PayerType payerType = PayerType.B2C)
        {
            PaymentMethod = paymentMethod;
            PayerType = payerType;
        }
    }

    public enum PaymentMethod
    {
        Cash,
        Cashless,
        Loan
    }
    
    public enum PayerType
    {
        B2B,
        B2C
    }
}
