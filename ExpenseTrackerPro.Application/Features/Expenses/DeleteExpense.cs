using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpenseTrackerPro.Application.Features.Expenses;

public class DeleteExpenseCommand: IRequest<Result<int>>
{

    public int Id { get; private set; }
    public DeleteExpenseCommand(int id)
    {
        Id = id;
    }
}

internal sealed class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteExpenseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
        var expense = await _unitOfWork.Repository<Expense>().GetByIdAsync(command.Id);

        await _unitOfWork.Repository<Expense>().DeleteAsync(expense);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(expense.Id, Messages.ExpenseDeleted.ToDescriptionString());
    }
}
