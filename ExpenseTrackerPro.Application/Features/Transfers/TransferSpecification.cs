using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class TransferSpecification : Specification<Transfer>
{
    public TransferSpecification(string searchString)
    {
        Includes.Add(a => a.Sender);
        Includes.Add(a => a.Receiver);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.Sender != null && p.Receiver != null) && (p.Sender.Name.Contains(searchString) || p.Receiver.Name.Contains(searchString) || p.Note.Contains(searchString));
        }
        else
        {
            Criteria = p => (p.Sender != null && p.Receiver != null);
        }
    }
}
