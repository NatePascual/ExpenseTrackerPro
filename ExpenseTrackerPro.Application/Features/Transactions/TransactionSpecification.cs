using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class TransactionSpecification : Specification<Transaction>
{
    public TransactionSpecification(int id)
    {
        Includes.Add(a => a.Account);

        if (id != 0)
        {
            Criteria = p => (p.Account != null) && (p.Account.Id == id);
        }
        else
        {
            Criteria = p => (p.Account != null);
        }
    }
}
