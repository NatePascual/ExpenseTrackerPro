using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;
public class Transfer : BaseAuditableEntity
{
    [ForeignKey(nameof(Account.Id))]
    public int FromAccountId { get; set; }
    public Account FromAccount { get; set; }

    [ForeignKey(nameof(Account.Id))]
    public int ToAccountId { get; set; }
    public Account ToAccount { get; set; }

    [Required]
    public float Amount { get; set; }

    public DateOnly TransactionDate { get; set; }

    [MaxLength(200)]
    public string Note { get; set; }

    public bool IsTransferAsExpense { get; set; } = true;

    public Transfer(int fromAccountId,
                    int toAccountId,
                    float amount,
                    DateOnly transactionDate,
                    string note,
                    bool isTransferAsExpense)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
        TransactionDate = transactionDate;
        Note = note;
        IsTransferAsExpense = isTransferAsExpense;
    }

    private Transfer() { }

}