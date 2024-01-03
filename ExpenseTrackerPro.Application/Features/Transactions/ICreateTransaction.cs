using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public interface ICreateTransaction
{
    string Message { get; set; }
    Task<bool> CreateTransactionAccount(Account account, CancellationToken cancellationToken);
    Task<bool> CreateTransactionTransfer(Transfer transfer, CancellationToken cancellationToken);
    Task<bool> CreateTransactionIncome(Income income, CancellationToken cancellationToken, bool isTransfer = false);
    Task<bool> CreateTransactionExpense(Expense expense, CancellationToken cancellationToken, bool isTransfer = false);
}