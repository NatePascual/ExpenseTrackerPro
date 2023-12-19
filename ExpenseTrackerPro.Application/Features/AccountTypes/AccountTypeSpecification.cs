using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.AccountTypes;

public class AccountTypeSpecification: Specification<AccountType>
{
    public AccountTypeSpecification(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.Name.Contains(searchString) || p.Classification.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }
}
