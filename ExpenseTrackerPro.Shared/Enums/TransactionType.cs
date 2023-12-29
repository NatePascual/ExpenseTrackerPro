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

public enum Messages
{
    [Description("Account Saved!")]
    AccountSaved,
    [Description("Account Updated!")]
    AccountUpdated,
    [Description("Account Deleted!")]
    AccountDeleted,
    [Description("Expense Saved!")]
    ExpenseSaved,
    [Description("Expense Updated!")]
    ExpenseUpdated,
    [Description("Expense Deleted!")]
    ExpenseDeleted,
    [Description("Income Saved!")]
    IncomeSaved,
    [Description("Income Updated!")]
    IncomeUpdated,
    [Description("Income Deleted!")]
    IncomeDeleted,
    [Description("Transfer Saved!")]
    TransferSaved,
    [Description("Transfer Updated!")]
    TransferUpdated,
    [Description("Transfer Deleted!")]
    TransferDeleted,
    [Description("Record Not Found!")]
    RecordNotFound,
    [Description("Account with Institution {0} and Number {1} already exists!")]
    AccountInstitutionAndNumberExists,
    [Description("Account doesn't Exist!")]
    AccountDoesntExist,
}