using System.Linq.Expressions;
using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Repositories;
using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Shared;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Order;

public class ActiveOrderWithLimitedProductThisQuarter : Specification<Order>
{
    private readonly Guid _customerId;
    private readonly string _limitedProductName;
    private readonly YearQuarter _thisQuarter = new(DateTime.UtcNow);

    public ActiveOrderWithLimitedProductThisQuarter(Guid customerId, string limitedProductName)
    {
        _customerId = customerId;
        _limitedProductName = limitedProductName;
    }
    
    protected override Expression<Func<Order, bool>> AsPredicateExpression()
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