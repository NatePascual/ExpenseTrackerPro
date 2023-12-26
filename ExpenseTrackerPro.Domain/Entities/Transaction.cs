using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    [ForeignKey(nameof(Account.Id))]
    public int AccountId { get; set; }
    public virtual Account Account { get; set; }
    public string TransactionType { get; set; }
    public bool AsSender { get; set; } = false;
    public bool AsReceiver { get; set; } = false;

    public DateOnly TransactionDate { get; set; }
    public float Amount { get; set; }

    [NotMapped]
    public string AmountToString
    {
        get
        {
            if (TransactionType == "Expense")
                return $"- {Amount.ToString()}";

            if (TransactionType == "Income")
                return $"+ {Amount.ToString()}";

            if (TransactionType == "Transfer" && AsReceiver && !AsSender)
                return $"+ {Amount.ToString()}";

            if (TransactionType == "Transfer" && !AsReceiver && AsSender)
                return $"- {Amount.ToString()}";

            return $"+ {Amount.ToString()}";
        }
    }

    public Transaction(int accountId, 
                       string transactionType,
                       DateOnly transactionDate,
                       float amount,
                       bool asSender,
                       bool asReceiver)
    {
        AccountId = accountId;
        TransactionType = transactionType;  
        TransactionDate= transactionDate;
        Amount = amount;
        AsReceiver = asReceiver;
        AsSender = asSender;
    }

}
