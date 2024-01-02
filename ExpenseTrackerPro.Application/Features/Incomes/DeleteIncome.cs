using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;

namespace ExpenseTrackerPro.Application.Features.Incomes;

public class DeleteIncomeCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public DeleteIncomeCommand(int id)
    {
        Id= id;
    }
}

internal sealed class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteIncomeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(DeleteIncomeCommand command, CancellationToken cancellationToken)
    {
        var income = await _unitOfWork.Repository<Income>().GetByIdAsync(command.Id);

        await _unitOfWork.Repository<Income>().DeleteAsync(income);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(income.Id, Messages.IncomeDeleted.ToDescriptionString());
    }
}