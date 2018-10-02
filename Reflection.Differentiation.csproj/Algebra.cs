using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Differentiation
{
    public static class Algebra
    {
        internal static Expression<Func<double, double>> Differentiate(Expression<Func<double, double>> function)
        {
            return Expression.Lambda<Func<double, double>>(function.Body.Differentiate(), function.Parameters);
        }
        private static MethodInfo CosCall = new Func<double, double>(Math.Cos).GetMethodInfo();
        private static MethodInfo SinCall = new Func<double, double>(Math.Sin).GetMethodInfo();

        public static Expression Differentiate(this Expression function)
        {
            Expression newBody = null;

            switch (function)
            {
                case ConstantExpression _:
                    newBody = Expression.Constant(0.0);
                    break;
                case ParameterExpression _:
                    newBody = Expression.Constant(1.0);
                    break;
                case BinaryExpression be when (be.NodeType == ExpressionType.Multiply):
                    newBody = Expression.Add(Expression.Multiply(be.Right.Differentiate(), be.Left), Expression.Multiply(be.Left.Differentiate(), be.Right));
                    break;
                case BinaryExpression be when (be.NodeType == ExpressionType.Divide):
                    newBody = be.Right;
                    break;
                case BinaryExpression be when (be.NodeType == ExpressionType.Add):
                    newBody = Expression.Add(be.Left.Differentiate(), be.Right.Differentiate());
                    break;
                case BinaryExpression be when (be.NodeType == ExpressionType.Subtract):
                    newBody = be.Right;
                    break;
                case MethodCallExpression be when (be.NodeType == ExpressionType.Call):
                    var args = be.Arguments.FirstOrDefault();
                    if(be.Method.Name == "Sin")
                        newBody = Expression.Multiply(args.Differentiate(), Expression.Call(CosCall, args));
                    if(be.Method.Name == "Cos")
                        newBody = Expression.Multiply(Expression.Constant(-1.0), Expression.Multiply(args.Differentiate(), Expression.Call(SinCall, args)));
                    break;
            }

            return newBody; 
        }
    }
}
