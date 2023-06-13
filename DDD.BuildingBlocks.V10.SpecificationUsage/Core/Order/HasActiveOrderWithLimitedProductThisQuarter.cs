using System.Linq.Expressions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;

public class HasActiveOrderWithLimitedProductThisQuarter : Specification<Order>
{
    private readonly Guid _customerId;
    private readonly string _limitedProductName;
    private readonly YearQuarter _thisQuarter = new(DateTime.UtcNow);

    public HasActiveOrderWithLimitedProductThisQuarter(Guid customerId, string limitedProductName)
    {
        _customerId = customerId;
        _limitedProductName = limitedProductName;
    }
    
    public override Expression<Func<Order, bool>> AsPredicateExpression()
    {
        var thisQuarterStartDate = _thisQuarter.GetQuarterStartDate();
        var nextQuarterStartDate = _thisQuarter.CreateFollowingQuarter().GetQuarterStartDate();
        
        return order => 
            order.CustomerId.Equals(_customerId) &&
            !order.Status.Equals(OrderStatus.Canceled) &&
            order.Lines.Any(x => x.Name.Equals(_limitedProductName)) &&
            order.PlaceDateUtc >= thisQuarterStartDate &&
            order.PlaceDateUtc < nextQuarterStartDate;
    }
}