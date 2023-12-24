using System.ComponentModel;

namespace ExpenseTrackerPro.Shared.Enums;

public static class ImageLocation
{
    public static string AccountType { get; } = @"Images/AccountType/";
    public static string Institution { get; } = @"Images/Institution/";
    public static string Category { get; } = @"Images/Category/";
    public static string IncomeCategory { get; } = @"Images/IncomeCategory/";
    public static string Expense { get; } = @"ImageTransactions/Expense/";
    public static string Income { get; } = @"ImageTransactions/Income/";
}