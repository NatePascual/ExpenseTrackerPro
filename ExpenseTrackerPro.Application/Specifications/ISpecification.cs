using ExpenseTrackerPro.Domain.Contracts;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Specifications;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    bool IsSatisfiedBy(T entity);
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    TrackerFilter Filter { get; set; }
    string ActualEntityName { get; set; }
    //Expression<Func<T, bool>> Predicate(string entityName, ITrackerFilter<T> filter);
}