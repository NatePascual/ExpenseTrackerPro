﻿using ExpenseTrackerPro.Domain.Contracts;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Specifications;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}