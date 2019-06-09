using System;
using System.Linq;
using System.Linq.Expressions;

namespace EZSpecification.Internal
{
    internal class ParameterVisitor<TResult> : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterExpression;
        private readonly Expression _replacementExpression;

        internal ParameterVisitor(ParameterExpression parameterExpression, Expression replacementExpression)
        {
            _parameterExpression = parameterExpression;
            _replacementExpression = replacementExpression;
        }

        internal Expression<TResult> Visit<T>(Expression<T> node)
        {
            var otherParameters = node.Parameters.Where(p => p != _parameterExpression);
            return Expression.Lambda<TResult>(Visit(node.Body), otherParameters);
        }

        protected override Expression VisitParameter(ParameterExpression node) =>
            node == _parameterExpression ? _replacementExpression : base.VisitParameter(node);
    }
}