using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;
public class Transfer : BaseAuditableEntity
{
    [ForeignKey(nameof(Account.Id))]
    public int SenderId { get; set; }
    public Account Sender { get; set; }

    [ForeignKey(nameof(Account.Id))]
    public int ReceiverId { get; set; }
    public Account Receiver { get; set; }

    [Required]
    public float Amount { get; set; }

    public DateOnly TransactionDate { get; set; }

    [MaxLength(200)]
    public string Note { get; set; }

    public bool IsTransferAsExpense { get; set; } = true;

    public Transfer(int senderId,
                    int receiverId,
                    float amount,
                    DateOnly transactionDate,
                    string note,
                    bool isTransferAsExpense)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Amount = amount;
        TransactionDate = transactionDate;
        Note = note;
        IsTransferAsExpense = isTransferAsExpense;
    }

    private Transfer() { }

}