using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public string Description {  get; set; }

    public virtual ICollection<JournalEntry> Entries { get; set; }
    public virtual ICollection<Account> Accounts { get; set; }

    public Transaction(DateTime transactionDate,
                       string description)
    {
        TransactionDate = transactionDate;
        Description = description;
        Entries = new List<JournalEntry>();
        Accounts = new List<Account>();
    }
}
