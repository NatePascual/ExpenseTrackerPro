using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Institutions;

public class InstitutionSpecification : Specification<Institution>
{
    public InstitutionSpecification(string searchString)
    {
        if(!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.Name.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }
}
