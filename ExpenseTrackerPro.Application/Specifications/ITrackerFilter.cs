namespace ExpenseTrackerPro.Application.Specifications;

public interface ITrackerFilter
{
    string Property { get; set; }
    string FilterValue { get; set; }
    Operators FirstOperator { get; set; }
    string SecondFilterValue { get; set; }
    Operators SecondOperator { get; set; }
    LogicalOperators LogicalOperator { get; set; }
}