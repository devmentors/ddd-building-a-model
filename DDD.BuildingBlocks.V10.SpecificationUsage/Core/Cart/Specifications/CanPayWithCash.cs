using System.Linq.Expressions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart.Specifications;

public class CanPayWithCash: Specification<Payment>
{
    private readonly decimal _paymentValue;

    public CanPayWithCash(decimal paymentValue)
    {
        _paymentValue = paymentValue;
    }
    
    public override Expression<Func<Payment, bool>> AsPredicateExpression()
    {
        return new CanPayWithCashForB2B(_paymentValue) | new CanPayWithCashForB2C(_paymentValue);
    }
}