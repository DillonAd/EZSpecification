using EZSpecification.Internal;
using System;
using System.Linq.Expressions;

namespace EZSpecification
{
    public static class SpecificationExtensions
    {
        public static Specification<TEntity> And<TEntity>(
            this Specification<TEntity> current, 
            Specification<TEntity> specification) where TEntity : class => 
                new CompoundSpecification<TEntity>(current.Expression.And(specification.Expression));
        
        public static Specification<TEntity> Or<TEntity>(
            this Specification<TEntity> current, 
            Specification<TEntity> specification) where TEntity : class =>
                new CompoundSpecification<TEntity>(current.Expression.Or(specification.Expression));
    }
}