using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.ExpenseCategories;

public class CategorySpecification : Specification<Category>
{
    public CategorySpecification(string searchString)
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
