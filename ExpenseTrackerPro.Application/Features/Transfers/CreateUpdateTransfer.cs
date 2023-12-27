using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Transactions;
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class CreateUpdateTransferCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Required]
    public int SenderId { get; set; }

    [Required]
    public int ReceiverId { get; set; }

    [Required]
    public float Amount { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public bool IsTransferAsExpense { get; set; } = true;
}

internal sealed class CreateUpdateTransferCommandHandler : IRequestHandler<CreateUpdateTransferCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUpdateTransferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUpdateTransferCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Transfer>(command);
            await _unitOfWork.Repository<Transfer>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            await ManageTransaction.AddAsync(_unitOfWork, entity.SenderId, TransactionType.Expense.ToDescriptionString(),
                                             entity.Created, entity.Amount, true, false, cancellationToken);

            await ManageTransaction.AddAsync(_unitOfWork, entity.ReceiverId, TransactionType.Expense.ToDescriptionString(),
                                            entity.Created, entity.Amount, false, true, cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, Messages.TransferSaved.ToDescriptionString());
        }
        else
        {
            var transfer = await _unitOfWork.Repository<Transfer>().GetByIdAsync(command.Id);   

            if(transfer !=null)
            {
                transfer.SenderId = (command.SenderId == 0) ? transfer.SenderId : command.SenderId;
                transfer.ReceiverId = (command.ReceiverId == 0) ? transfer.ReceiverId : command.ReceiverId;
                transfer.Amount = (command.Amount == 0) ? transfer.Amount : command.Amount;
                transfer.TransactionDate = (command.TransactionDate != transfer.TransactionDate) ? command.TransactionDate : transfer.TransactionDate;
                transfer.Note = command.Note ?? transfer.Note;
                transfer.IsTransferAsExpense = transfer.IsTransferAsExpense;

                var entity = _mapper.Map<Transfer>(transfer);
                await _unitOfWork.Repository<Transfer>().UpdateAsync(entity);
                await _unitOfWork.Commit(cancellationToken);

                result = await Result<int>.SuccessAsync(entity.Id, Messages.TransferUpdated.ToDescriptionString());
            }
            else
            {
                result = await Result<int>.FailAsync(Messages.RecordNotFound.ToDescriptionString());
            }
        }

        return result;
    }
}