using System;
using System.Linq.Expressions;

namespace EZSpecification
{
    public interface ISpecification<TEntity>
        where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> Expression { get; }
    }
}
