using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public enum Operators
{
    Contains,
    Equal,
    NotEqual,
    GreaterThan,
    LessThan,
    // Add more operators as needed
}

public enum LogicalOperators
{
    And,
    Or
}

public class MySpecification<T>
{
    private readonly List<Expression<Func<T, bool>>> conditions = new List<Expression<Func<T, bool>>>();

    public MySpecification<T> AddCondition(Expression<Func<T, object>> property, Operators filterOperator, object value)
    {
        var propertyExpression = property.Body as MemberExpression;
        if (propertyExpression == null)
            throw new ArgumentException("Invalid property expression");

        var parameter = property.Parameters.Single();
        var propertyAccess = Expression.MakeMemberAccess(parameter, propertyExpression.Member);
        var condition = GetComparisonExpression(propertyAccess, filterOperator, value);
        conditions.Add(Expression.Lambda<Func<T, bool>>(condition, parameter));

        return this;
    }

    public MySpecification<T> CombineWith(LogicalOperators logicalOperator)
    {
        if (conditions.Count < 2)
            throw new InvalidOperationException("Cannot combine with logical operator without multiple conditions.");

        var combinedCondition = CombineConditions(logicalOperator);
        conditions.Clear();
        conditions.Add(combinedCondition);

        return this;
    }

    public Expression<Func<T, bool>> Build()
    {
        if (conditions.Count == 0)
            throw new InvalidOperationException("No conditions specified for the specification.");

        return conditions.Count == 1 ? conditions.Single() : CombineConditions(LogicalOperators.And);
    }

    private Expression<Func<T, bool>> CombineConditions(LogicalOperators logicalOperator)
    {
        var combinedCondition = conditions.First();
        var parameter = combinedCondition.Parameters.Single();

        foreach (var condition in conditions.Skip(1))
        {
            var body = Expression.MakeBinary(
                logicalOperator == LogicalOperators.And ? ExpressionType.AndAlso : ExpressionType.OrElse,
                combinedCondition.Body,
                condition.Body);

            combinedCondition = Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        return combinedCondition;
    }

    private Expression GetComparisonExpression(MemberExpression property, Operators filterOperator, object value)
    {
        var constant = Expression.Constant(value);
        Expression comparison;

        switch (filterOperator)
        {
            case Operators.Contains:
                comparison = Expression.Call(property, "Contains", null, constant);
                break;
            case Operators.Equal:
                comparison = Expression.Equal(property, constant);
                break;
            case Operators.NotEqual:
                comparison = Expression.NotEqual(property, constant);
                break;
            case Operators.GreaterThan:
                comparison = Expression.GreaterThan(property, constant);
                break;
            case Operators.LessThan:
                comparison = Expression.LessThan(property, constant);
                break;
            // Add more cases as needed
            default:
                throw new ArgumentOutOfRangeException(nameof(filterOperator), "Unsupported filter operator");
        }

        return comparison;
    }
}
