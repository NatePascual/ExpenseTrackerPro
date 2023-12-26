
using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Account : BaseAuditableEntity
{
    [ForeignKey(nameof(AccountType.Id))]
    public int AccountTypeId { get; set; }
    public virtual AccountType AccountType { get; set; }

    [ForeignKey(nameof(Institution.Id))]
    public int InstitutionId { get; set; }
    public virtual Institution Institution { get; set; }

    [ForeignKey(nameof(Currency.Id))]
    public int CurrencyId { get; set; }
    public virtual Currency Currency { get; set; }

    [Length(10, 30), Required]
    public string Name { get; set; }

    [MaxLength(4), Required]
    public string AccountNumber { get; set; }

    [Required]
    public float Balance { get; set; }

    public bool IsIncludedBalance { get; set; } = false;

    public virtual ICollection<Transfer> Senders { get; set; }
    public virtual ICollection<Transfer> Receivers { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }

    public Account(int accountTypeId, 
                   int institutionId, 
                   int currencyId,
                   string name, 
                   string accountNumber,               
                   float balance, 
                   bool isIncludedBalance)
    {
        AccountTypeId = accountTypeId;
        InstitutionId = institutionId;
        CurrencyId = currencyId;
        Name = name;
        AccountNumber = accountNumber;
        Balance = balance;
        IsIncludedBalance = isIncludedBalance;
    }

    private Account() { }
}
