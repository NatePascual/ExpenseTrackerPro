using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Income : BaseAuditableEntity
{
    [ForeignKey(nameof(IncomeCategory.Id))]
    public int IncomeCategoryId { get; set; }
    public virtual IncomeCategory IncomeCategory { get; set; }

    [ForeignKey(nameof(Account.Id))]
    public int AccountId { get; set; }
    public virtual Account Account { get; set; }

    [Required]
    public float Amount { get; set; }
    public DateTime? TransactionDate { get; set; }

    [MaxLength(200)]
    public string Note { get; set; }

    [MaxLength(200)]
    public string Photo { get; set; }

    public Income(int incomeCategory,
                  int accountId,
                  float amount,
                  DateTime? transactionDate,
                  string note,
                  string photo) 
    {
        IncomeCategoryId = incomeCategory;
        AccountId = accountId;
        Amount = amount;
        TransactionDate = transactionDate;
        Note = note;
        Photo = photo;
    }

    private Income()
    {

    }
}
