using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class JournalEntry:BaseAuditableEntity
{
    [ForeignKey(nameof(Account.Id))]
    public int AccountId { get; set; }
    public Account Account { get; set; }

    [ForeignKey(nameof(Transaction.Id))]
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; }

    [Required]
    public float Amount {  get; set; }

    [Required]
    public bool IsDebit {  get; set; }

    public JournalEntry(int accountId, 
                        int transactionId,
                        float amount,
                        bool isDebit) 
    {
        AccountId = accountId;
        TransactionId = transactionId;
        Amount = amount;
        IsDebit = isDebit;
    }
}
