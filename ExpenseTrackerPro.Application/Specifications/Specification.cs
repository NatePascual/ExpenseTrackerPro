﻿using ExpenseTrackerPro.Domain.Contracts;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Specifications;

public abstract class Specification<T> : ISpecification<T> where T : class, IEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}
