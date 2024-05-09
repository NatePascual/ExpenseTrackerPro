using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        if (left == null) return right;
        var parameter = Expression.Parameter(typeof(T));
        var and = Expression.AndAlso(left.Body, right.Body);
        return Expression.Lambda<Func<T, bool>>(and, parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        if (left == null) return right;
        var parameter = Expression.Parameter(typeof(T));
        var or = Expression.OrElse(left.Body, right.Body);
        return Expression.Lambda<Func<T, bool>>(or, parameter);
    }
}
