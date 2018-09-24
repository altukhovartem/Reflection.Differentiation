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
            var qwe = function.Compile();
            var xx = qwe(10);
            ExpressionType expType = function.Body.NodeType; 
            switch (expType)
            {
                //case (ExpressionType.Constant): return 0; break; 

            }

            throw new NotImplementedException();
        }
    }
}
