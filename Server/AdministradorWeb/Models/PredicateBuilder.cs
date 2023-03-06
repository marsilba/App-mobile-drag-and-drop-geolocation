using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdministradorWeb.Models
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null || expr2 == null)
                throw new ArgumentNullException("Valor da funcao nao pode ser null");

            return expr1.Compose(expr2, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null || expr2 == null)
                throw new ArgumentNullException("Valor da funcao nao pode ser null");

            return expr1.Compose(expr2, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            if (first == null || second == null)
                throw new ArgumentNullException("Valor da funcao nao pode ser null");

            var map = first.Parameters
                           .Select((f, i) => new { f, s = second.Parameters[i] })
                           .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                if (map.TryGetValue(p, out ParameterExpression replacement))
                    p = replacement;

                return base.VisitParameter(p);
            }
        }
    }
}