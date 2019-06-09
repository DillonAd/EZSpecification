using System;
using System.Linq.Expressions;

namespace EZSpecification.Internal
{
    internal static class ExpressionExtensions
    {
        internal static Expression<Func<TEntity, bool>> And<TEntity>(
            this Expression<Func<TEntity, bool>> left, 
            Expression<Func<TEntity, bool>> right) where TEntity : IEntity =>
            Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(left.Body, right.WithParameters<TEntity>(left).Body), left.Parameters);

        internal static Expression<Func<TEntity, bool>> Or<TEntity>(
            this Expression<Func<TEntity, bool>> left, 
            Expression<Func<TEntity, bool>> right) where TEntity : IEntity =>
            Expression.Lambda<Func<TEntity, bool>>(Expression.Or(left.Body, right.WithParameters<TEntity>(left).Body), left.Parameters);

        private static Expression<Func<bool>> WithParameters<TEntity>(
            this Expression<Func<TEntity, bool>> original,
            Expression<Func<TEntity, bool>> additional) where TEntity : IEntity =>
                new ParameterVisitor<Func<bool>>(original.Parameters[0], additional.Parameters[0]).Visit(original);
    }
}