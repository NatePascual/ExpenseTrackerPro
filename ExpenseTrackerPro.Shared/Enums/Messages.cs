using System.ComponentModel;

namespace ExpenseTrackerPro.Shared.Enums;


public enum Messages
{
    [Description("Account Saved!")]
    AccountSaved,
    [Description("Account Updated!")]
    AccountUpdated,
    [Description("Account Deleted!")]
    AccountDeleted,
    [Description("Account Transaction Error!")]
    AccountTransactionError,
    [Description("Expense Saved!")]
    ExpenseSaved,
    [Description("Expense Updated!")]
    ExpenseUpdated,
    [Description("Expense Deleted!")]
    ExpenseDeleted,
    [Description("Expense Transaction Error!")]
    ExpenseTransactionError,
    [Description("Income Saved!")]
    IncomeSaved,
    [Description("Income Updated!")]
    IncomeUpdated,
    [Description("Income Deleted!")]
    IncomeDeleted,
    [Description("Income Transaction Error!")]
    IncomeTransactionError,
    [Description("Transfer Saved!")]
    TransferSaved,
    [Description("Transfer Updated!")]
    TransferUpdated,
    [Description("Transfer Deleted!")]
    TransferDeleted,
    [Description("Transfer Transaction Error!")]
    TransferTransactionError,
    [Description("Record Not Found!")]
    RecordNotFound,
    [Description("Account with Institution {0} and Number {1} already exists!")]
    AccountInstitutionAndNumberExists,
    [Description("Account doesn't Exist!")]
    AccountDoesntExist,
    [Description("Deletion Not Allowed!")]
    DeletionNotAllowed,
    [Description("Debit or Credit Error!")]
    DebitOrCreditError,
    [Description("Journal Entry Error!")]
    JournalEntryError,
  
    
  
}