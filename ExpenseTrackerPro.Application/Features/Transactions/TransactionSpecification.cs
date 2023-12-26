using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class TransactionSpecification : Specification<Transaction>
{
    public TransactionSpecification(string searchString)
    {
        Includes.Add(a => a.Account);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.Account != null) && (p.Account.Name.Contains(searchString) || p.Account.AccountNumber.Contains(searchString));
        }
        else
        {
            Criteria = p => (p.Account != null);
        }
    }
}
