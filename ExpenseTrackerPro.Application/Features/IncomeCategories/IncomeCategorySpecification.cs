using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.IncomeCategories;

public class IncomeCategorySpecification : Specification<IncomeCategory>
{
    public IncomeCategorySpecification(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.Name.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }
}
