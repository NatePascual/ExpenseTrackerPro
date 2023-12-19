using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class TransferSpecification : Specification<Transfer>
{
    public TransferSpecification(string searchString)
    {
        Includes.Add(a => a.FromAccount);
        Includes.Add(a => a.ToAccount);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.FromAccount != null && p.ToAccount != null) && (p.FromAccount.Name.Contains(searchString) || p.ToAccount.Name.Contains(searchString) || p.Note.Contains(searchString));
        }
        else
        {
            Criteria = p => (p.FromAccount != null && p.ToAccount != null);
        }
    }
}
