using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MergeLinqExpressions.Helpers
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Merge an expression with the current expression.
        /// </summary>
        /// <typeparam name="T">Expression delegate type. Both expressions must have the same delegate type.</typeparam>
        /// <param name="mainExpression">Current expression.</param>
        /// <param name="addExpression">Expression to merge.</param>
        /// <param name="bynaryExpression">Operator between expression. Use a BynaryExpression.</param>
        /// <returns>An expression which execute a logic operator with the added expression.</returns>
        public static Expression<T> Merge<T>(this Expression<T> mainExpression, Expression<T> addExpression, Func<Expression , Expression, Expression> bynaryExpression)
            where T : class
        {
            // build parameter map (from parameters of second to parameters of first)
            Dictionary<ParameterExpression, ParameterExpression> map = mainExpression.Parameters.Select((f, i) => new { f, s = addExpression.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            Expression secondBody = ExpressionParameterRebinder.ReplaceParameters(map, addExpression.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(bynaryExpression(mainExpression.Body, secondBody), mainExpression.Parameters);
        }
    }
}
