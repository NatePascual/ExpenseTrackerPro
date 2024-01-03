using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class TransactionSpecification : Specification<Transaction>
{
    public TransactionSpecification(int id)
    {
        if (id > 0)
        {
            Criteria = p => p.Accounts.Any(x=> x.JournalEntries.Any(x=>x.TransactionId == id));
        }
        else
        {
            Criteria = p => true;
        }
    }
}
