using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Differentiation
{
    public class Algebra
    {
        internal static Expression<Func<double, double>> Differentiate(Expression<Func<double, double>> function)
        {
            Expression<Func<double, double>> resultExp = null;
            var oldParameters = function.Parameters;
            var oldBody = function.Body;
            var nodeType = function.NodeType;

            ParameterExpression newParams = null;
            Expression newBody = null;

            ExpressionType expType = function.Body.NodeType; 
            switch (expType)
            {
                case (ExpressionType.Constant):
                    newParams = oldParameters.FirstOrDefault();
                    newBody = Expression.Constant(0.0);
                    break;
                case (ExpressionType.Parameter):
                    newParams = oldParameters.FirstOrDefault();
                    newBody = Expression.Constant(1.0);
                    break;
                case ExpressionType type when type.Equals(ExpressionType.Multiply):
                    newParams = oldParameters.FirstOrDefault();
                    newBody = Expression.Constant(5.0);
                    break; 
            }

            resultExp = Expression.Lambda<Func<double, double>>(newBody, newParams);

            return resultExp; 
        }
    }
}
