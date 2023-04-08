using System.Linq.Expressions;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Repositories;

public abstract class Specification<T>
{
    protected abstract Expression<Func<T, bool>> AsPredicateExpression();
    
    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.AsPredicateExpression();
    }
}