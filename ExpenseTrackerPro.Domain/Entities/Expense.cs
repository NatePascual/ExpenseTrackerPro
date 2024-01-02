using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Expense : BaseAuditableEntity
{
    [ForeignKey(nameof(Category.Id))]
    public int CategoryId { get; set; }
    public virtual Category ExpenseCategory { get; set; }

    [ForeignKey(nameof(Account.Id))]
    public int AccountId { get; set; }
    public Account Account { get; set; }

    public string Provider { get; set; } = null;

    [Required]
    public DateTime? TransactionDate { get; set; }

    [Required]
    public float Amount { get; set; }

    [MaxLength(200)]
    public string Note { get; set; }

    [MaxLength(200)]
    public string Photo { get; set; }

    public Expense(int categoryId, 
                   int accountId, 
                   string provider,
                   DateTime? transactionDate,
                   string note,
                   string photo) 
    {
        CategoryId = categoryId;
        AccountId = accountId;
        Provider = provider;
        TransactionDate = transactionDate;
        Note = note;
        Photo = photo;
    }
    private Expense() { }
}
