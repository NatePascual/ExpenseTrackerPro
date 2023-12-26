using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;


namespace ExpenseTrackerPro.Application.Features.Transactions;

public static class ManageTransaction
{
    public static async Task AddAsync(IUnitOfWork unitOfWork,
                                  int id,
                                  string transactionType,
                                  DateOnly transactionDate,
                                  float amount,
                                  bool asSender,
                                  bool asReceiver,
                                  CancellationToken cancellationToken)
    {
        Transaction transaction = new Transaction(id, transactionType, transactionDate, amount, asSender, asReceiver);
        await unitOfWork.Repository<Transaction>().AddAsync(transaction);
        await unitOfWork.Commit(cancellationToken);
    }
}
