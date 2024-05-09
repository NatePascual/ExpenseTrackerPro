using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Contracts;
using Microsoft.IdentityModel.Tokens;
using Radzen;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Specifications;

public class TrackerFilter: ITrackerFilter
{
    /// <summary>
    /// Gets or sets the name of the filtered property.
    /// </summary>
    /// <value>The property.</value>
    public string Property { get; set; }
    /// <summary>
    /// Gets or sets the value to filter by.
    /// </summary>
    /// <value>The filter value.</value>
    public string FilterValue { get; set; }
    /// <summary>
    /// Gets or sets the operator which will compare the property value with <see cref="FilterValue" />.
    /// </summary>
    /// <value>The filter operator.</value>
    public Operators FirstOperator { get; set; }
    /// <summary>
    /// Gets or sets a second value to filter by.
    /// </summary>
    /// <value>The second filter value.</value>
    public string SecondFilterValue { get; set; }
    /// <summary>
    /// Gets or sets the operator which will compare the property value with <see cref="SecondFilterValue" />.
    /// </summary>
    /// <value>The second filter operator.</value>
    public Operators SecondOperator { get; set; }
    /// <summary>
    /// Gets or sets the logic used to combine the outcome of filtering by <see cref="FilterValue" /> and <see cref="SecondFilterValue" />.
    /// </summary>
    /// <value>The logical filter operator.</value>
    public LogicalOperators LogicalOperator { get; set; }

    
}


public enum Operators
{
    /// <summary>
    /// Satisfied if the current value equals the specified value.
    /// </summary>
    Equals,
    /// <summary>
    /// Satisfied if the current value does not equal the specified value.
    /// </summary>
    NotEquals,
    /// <summary>
    /// Satisfied if the current value is less than the specified value.
    /// </summary>
    LessThan,
    /// <summary>
    /// Satisfied if the current value is less than or equal to the specified value.
    /// </summary>
    LessThanOrEquals,
    /// <summary>
    /// Satisfied if the current value is greater than the specified value.
    /// </summary>
    GreaterThan,
    /// <summary>
    /// Satisfied if the current value is greater than or equal to the specified value.
    /// </summary>
    GreaterThanOrEquals,
    /// <summary>
    /// Satisfied if the current value contains the specified value.
    /// </summary>
    Contains,
    /// <summary>
    /// Satisfied if the current value starts with the specified value.
    /// </summary>
    StartsWith,
    /// <summary>
    /// Satisfied if the current value ends with the specified value.
    /// </summary>
    EndsWith,
    /// <summary>
    /// Satisfied if the current value does not contain the specified value.
    /// </summary>
    DoesNotContain,
    /// <summary>
    /// Satisfied if the current value is null.
    /// </summary>
    In,
    /// <summary>
    /// Satisfied if the current value is in the specified value.
    /// </summary>
    NotIn,
    /// <summary>
    /// Satisfied if the current value is not in the specified value.
    /// </summary>
    IsNull,
    /// <summary>
    /// Satisfied if the current value is <see cref="string.Empty"/>.
    /// </summary>
    IsEmpty,
    /// <summary>
    /// Satisfied if the current value is not null.
    /// </summary>
    IsNotNull,
    /// <summary>
    /// Satisfied if the current value is not <see cref="string.Empty"/>.
    /// </summary>
    IsNotEmpty,
    /// <summary>
    /// Custom operator if not need to generate the filter.
    /// </summary>
    Custom
}

public enum LogicalOperators
{
    /// <summary>
    /// All filters should be satisfied.
    /// </summary>
    And,
    /// <summary>
    /// Any filter should be satisfied.
    /// </summary>
    Or
}