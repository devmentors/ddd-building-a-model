using System.Linq.Expressions;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> AsPredicateExpression();
    
    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.AsPredicateExpression();
    }

    public bool Check(T obj)
    {
        return AsPredicateExpression().Compile().Invoke(obj);
    }
}