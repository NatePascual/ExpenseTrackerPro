using System.ComponentModel;

namespace ExpenseTrackerPro.Shared.Enums;

public enum TransactionType
{
    [Description("Starting Balance")]
    StartingBalance,
    [Description("Expense")]
    Expense,
    [Description("Income")]
    Income,
    [Description("Transfer")]
    Transfer
}
