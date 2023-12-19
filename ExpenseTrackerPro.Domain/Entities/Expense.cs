using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Expense : BaseAuditableEntity
{
    [ForeignKey(nameof(Category.Id))]
    public int CategoryId { get; set; }
    public virtual Category ExpenseCategory { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }

    public string Provider { get; set; } = null;

    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public DateOnly TransactionDate { get; set; }

    [Required]
    public float Amount { get; set; }

    [MaxLength(200)]
    public string Note { get; set; }

    [MaxLength(200)]
    public string Photo { get; set; }

    public Expense(int categoryId, 
                   int accountId, 
                   string provider,
                   string title,
                   DateOnly transactionDate,
                   string note,
                   string photo) 
    {
        CategoryId = categoryId;
        AccountId = accountId;
        Provider = provider;
        Title = title;
        TransactionDate = transactionDate;
        Note = note;
        Photo = photo;
    }
    private Expense() { }
}
