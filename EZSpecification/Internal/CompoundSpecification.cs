using System;
using System.Linq.Expressions;

namespace EZSpecification.Internal
{
    public class CompoundSpecification<TEntity> : Specification<TEntity>
        where TEntity : class
    {
        public override Expression<Func<TEntity, bool>> Expression { get; }

        public CompoundSpecification(Expression<Func<TEntity, bool>> compoundSpecification)
        {
            Expression = compoundSpecification;
        }
    }
}