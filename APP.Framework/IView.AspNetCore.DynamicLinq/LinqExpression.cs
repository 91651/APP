using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IView.AspNetCore.DynamicLinq
{
    public static class LinqExpression
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, expr1.Parameters)), expr1.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, Expression.Invoke(expr2, expr1.Parameters)), expr1.Parameters);
        }
    }
}
