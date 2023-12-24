using System.ComponentModel;

namespace ExpenseTrackerPro.Domain.Enums;

public enum UploadType
{
    [Description(@"Images\AccountType")]
    AccountType,

    [Description(@"Images\Institution")]
    Institution,

    [Description(@"Images\Category")]
    Category,

    [Description(@"Images\IncomeCategory")]
    IncomeCategory,

    [Description(@"ImageTransactions\Expense")]
    Expense,

    [Description(@"ImageTransactions\Income")]
    Income,
}

