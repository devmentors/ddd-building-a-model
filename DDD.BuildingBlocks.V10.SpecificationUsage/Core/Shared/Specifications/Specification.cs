using System.Linq.Expressions;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> AsPredicateExpression();
    
    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.AsPredicateExpression();
    }
    
    public static OrSpecification<T> operator |(Specification<T> left, Specification<T> right)
    {
        return left.Or(right);
    }
    public static AndSpecification<T> operator &(Specification<T> left, Specification<T> right)
    {
        return left.And(right);
    }
    

    public bool Check(T obj)
    {
        return AsPredicateExpression().Compile().Invoke(obj);
    }
}