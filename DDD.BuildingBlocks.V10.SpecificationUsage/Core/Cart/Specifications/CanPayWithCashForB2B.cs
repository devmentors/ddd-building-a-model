using System.Linq.Expressions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart.Specifications;

public class CanPayWithCashForB2B : Specification<Payment>
{
    private readonly decimal _paymentValue;

    public CanPayWithCashForB2B(decimal paymentValue)
    {
        _paymentValue = paymentValue;
    }
    
    public override Expression<Func<Payment, bool>> AsPredicateExpression()
    {
        return x => x.PayerType == PayerType.B2B
                    && x.PaymentMethod == PaymentMethod.Cash
                    && _paymentValue < 20_000;
    }
}