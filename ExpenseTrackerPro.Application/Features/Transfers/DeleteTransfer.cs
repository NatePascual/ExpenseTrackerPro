using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class DeleteTransferCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public DeleteTransferCommand(int id)
    {
        Id=id;
    }
}
internal sealed class DeleteTransferCommandHandler : IRequestHandler<DeleteTransferCommand,Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteTransferCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(DeleteTransferCommand command, CancellationToken cancellationToken)
    {
        var transfer = await _unitOfWork.Repository<Transfer>().GetByIdAsync(command.Id);

        await _unitOfWork.Repository<Transfer>().DeleteAsync(transfer);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(transfer.Id, Messages.TransferDeleted.ToDescriptionString());
    }
}
