using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class AccountSpecification:Specification<Account>
{
    public AccountSpecification(string searchString)
    {
        Includes.Add(a => a.AccountType);
        Includes.Add(a => a.Institution);
        Includes.Add(a => a.Currency);

        if(!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.AccountType != null && p.Institution != null && p.Currency != null) &&
                            (p.Name.Contains(searchString) || p.Institution.Name.Contains(searchString) ||
                             p.AccountType.Name.Contains(searchString) ||  p.Currency.CountryCurrency.Contains(searchString) ||
                             p.Currency.Code.Contains(searchString)) && p.IsHidden == false;
        }
        else
        {
            Criteria = p => p.AccountType != null && p.IsHidden == false;
        }
    }
}
