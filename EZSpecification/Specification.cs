using System;
using System.Linq.Expressions;

namespace EZSpecification
{
    public abstract class Specification<TEntity>
        where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> Expression { get; set; }

        public static implicit operator Func<TEntity, bool>(Specification<TEntity> specification) =>
            specification.Expression.Compile();
    }
}