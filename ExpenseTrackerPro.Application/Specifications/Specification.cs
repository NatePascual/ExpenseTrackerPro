using ExpenseTrackerPro.Domain.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Specifications;

public abstract class Specification<T> : ISpecification<T> where T : class, IEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public TrackerFilter Filter { get; set; }
    public string ActualEntityName {  get; set; }
    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public bool IsSatisfiedBy(T entity)
    {
        var propertyValue = entity.GetType().GetProperty(ActualEntityName)?.GetValue(entity, null);

        bool firstCondition = EvaluateCondition(propertyValue, Filter.FilterValue, Filter.FirstOperator);
        //bool firstCondition = EvaluateCondition(filter.Property, filter.FilterValue, filter.FirstOperator);

        if (Filter.SecondFilterValue != null)
        {
            bool secondCondition = EvaluateCondition(ActualEntityName, Filter.SecondFilterValue, Filter.SecondOperator);

            if (Filter.LogicalOperator == LogicalOperators.And)
            {
                return firstCondition && secondCondition;
            }
            else if (Filter.LogicalOperator == LogicalOperators.Or)
            {
                return firstCondition || secondCondition;
            }
        }

        return firstCondition;
    }

    private bool EvaluateCondition(object propertyValue, object filterValue, Operators filterOperator)
    {
        switch (filterOperator)
        {
            case Operators.Contains:
                return propertyValue.ToString().Contains(filterValue.ToString());
            case Operators.DoesNotContain:
                return !propertyValue.ToString().Contains(filterValue.ToString());
            case Operators.StartsWith:
                return propertyValue.ToString().StartsWith(filterValue.ToString());
            case Operators.EndsWith:
                return propertyValue.ToString().EndsWith(filterValue.ToString());
            case Operators.Equals:
                return propertyValue.Equals(filterValue);
            case Operators.NotEquals:
                return !propertyValue.Equals(filterValue);
            case Operators.GreaterThan:
                return Comparer.Default.Compare(propertyValue, filterValue) > 0;
            case Operators.LessThan:
                return Comparer.Default.Compare(propertyValue, filterValue) < 0;
            case Operators.GreaterThanOrEquals:
                return Comparer.Default.Compare(propertyValue, filterValue) >= 0;
            case Operators.LessThanOrEquals:
                return Comparer.Default.Compare(propertyValue, filterValue) <= 0;
            case Operators.In:
                return propertyValue.ToString().Contains(filterValue.ToString());
            case Operators.NotIn:
                return !propertyValue.ToString().Contains(filterValue.ToString());
            case Operators.IsNull:
                return propertyValue == null;
            case Operators.IsNotNull:
                return propertyValue != null;
            case Operators.IsEmpty:
                return propertyValue.ToString().IsNullOrEmpty();
            case Operators.IsNotEmpty:
                return !propertyValue.ToString().IsNullOrEmpty();
            // Add more cases for other operators as needed
            default:
                throw new ArgumentException("Unsupported filter operator");
        }
    }

    protected virtual void ModifyEntityName()
    {

    }
    //private Expression<Func<T, bool>> LeftPredicate { get; set; }
    //private Expression<Func<T, bool>> RightPredicate { get; set; }

    //private Expression<Func<T, bool>> LeftValue(string entityName, string value)
    //{
    //    switch (this.FilterOperator)
    //    {
    //        case "Contains":
    //            LeftPredicate = p => (entityName).Contains(value);
    //            break;
    //        case "DoesNotContain":
    //            LeftPredicate = p => !(entityName).Contains(value);
    //            break;
    //        case "StartsWith":
    //            LeftPredicate = p => (entityName).StartsWith(value);
    //            break;
    //        case "EndsWith":
    //            LeftPredicate = p => (entityName).EndsWith(value);
    //            break;
    //        case "Equals":
    //            LeftPredicate = p => (entityName) == value;
    //            break;
    //        case "NotEquals":
    //            LeftPredicate = p => (entityName) != value;
    //            break;
    //        case "LessThan":
    //            LeftPredicate = p => Convert.ToDouble(entityName) < Convert.ToDouble(value);
    //            break;
    //        case "GreaterThan":
    //            LeftPredicate = p => Convert.ToDouble(entityName) > Convert.ToDouble(value);
    //            break;
    //        case "LessThanOrEquals":
    //            LeftPredicate = p => Convert.ToDouble(entityName) <= Convert.ToDouble(value);
    //            break;
    //        case "GreaterThanOrEquals":
    //            LeftPredicate = p => Convert.ToDouble(entityName) >= Convert.ToDouble(value);
    //            break;
    //        case "In":
    //            LeftPredicate = p => (entityName).Contains(value);
    //            break;
    //        case "NotIn":
    //            LeftPredicate = p => !(entityName).Contains(value);
    //            break;
    //        case "IsNull":
    //            LeftPredicate = p => (entityName) == null;
    //            break;
    //        case "IsNotNull":
    //            LeftPredicate = p => (entityName) != null;
    //            break;
    //        case "IsEmpty":
    //            LeftPredicate = p => (entityName) == string.Empty;
    //            break;
    //        case "IsNotEmpty":
    //            LeftPredicate = p => (entityName) != string.Empty;
    //            break;
    //    }

    //    return Expression.Lambda<Func<T, bool>>(LeftPredicate.Body, LeftPredicate.Parameters.Single());
    //}

    //private Expression<Func<T, bool>> RightValue(string entityName, string value)
    //{
    //    if (SecondFilterValue == null) return null;

    //    switch (this.SecondFilterOperator)
    //    {
    //        case "Contains":
    //            RightPredicate = p => (entityName).Contains(value);
    //            break;
    //        case "DoesNotContain":
    //            RightPredicate = p => !(entityName).Contains(value);
    //            break;
    //        case "StartsWith":
    //            RightPredicate = p => (entityName).StartsWith(value);
    //            break;
    //        case "EndsWith":
    //            RightPredicate = p => (entityName).EndsWith(value);
    //            break;
    //        case "Equals":
    //            RightPredicate = p => (entityName) == value;
    //            break;
    //        case "NotEquals":
    //            RightPredicate = p => (entityName) != value;
    //            break;
    //        case "LessThan":
    //            RightPredicate = p => Convert.ToDouble(entityName) < Convert.ToDouble(value);
    //            break;
    //        case "GreaterThan":
    //            RightPredicate = p => Convert.ToDouble(entityName) > Convert.ToDouble(value);
    //            break;
    //        case "LessThanOrEquals":
    //            RightPredicate = p => Convert.ToDouble(entityName) <= Convert.ToDouble(value);
    //            break;
    //        case "GreaterThanOrEquals":
    //            RightPredicate = p => Convert.ToDouble(entityName) >= Convert.ToDouble(value);
    //            break;
    //        case "In":
    //            RightPredicate = p => (entityName).Contains(value);
    //            break;
    //        case "NotIn":
    //            RightPredicate = p => !(entityName).Contains(value);
    //            break;
    //        case "IsNull":
    //            RightPredicate = p => (entityName) == null;
    //            break;
    //        case "IsNotNull":
    //            RightPredicate = p => (entityName) != null;
    //            break;
    //        case "IsEmpty":
    //            RightPredicate = p => (entityName) == string.Empty;
    //            break;
    //        case "IsNotEmpty":
    //            RightPredicate = p => (entityName) != string.Empty;
    //            break;
    //    }


    //    return Expression.Lambda<Func<T, bool>>(RightPredicate.Body, RightPredicate.Parameters.Single());
    //}

    //public Expression<Func<T, bool>> Predicate(string entityName, ITrackerFilter<T> filter)
    //{
    //    this.FilterValue = filter.FilterValue;
    //    this.FilterOperator = filter.FilterOperator;
    //    this.Property = filter.Property;
    //    this.SecondFilterValue = filter.SecondFilterValue;
    //    this.SecondFilterOperator = filter.SecondFilterOperator;
    //    this.LogicalFilterOperator = filter.LogicalFilterOperator;

    //    if (SecondFilterValue == null)
    //        return LeftValue(entityName, FilterValue);

    //    if (LogicalFilterOperator == "Or")
    //    {
    //        return LeftValue(entityName, FilterValue).Or<T>(RightValue(entityName, SecondFilterValue));
    //    }

    //    return LeftValue(entityName, FilterValue).And<T>(RightValue(entityName, SecondFilterValue));

    //}
}

