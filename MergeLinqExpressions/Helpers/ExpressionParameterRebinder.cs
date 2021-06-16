using System.Collections.Generic;
using System.Linq.Expressions;

namespace MergeLinqExpressions.Helpers
{
    public class ExpressionParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        internal ExpressionParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Replace parameters in lambda from a parameter map.
        /// </summary>
        /// <param name="map">Parameter map which include lambda parameters.</param>
        /// <param name="exp">Lambda expression whom parameters will be rebinded.</param>
        /// <returns>An expression with rebinded parameters.</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ExpressionParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (map.TryGetValue(p, out ParameterExpression replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
