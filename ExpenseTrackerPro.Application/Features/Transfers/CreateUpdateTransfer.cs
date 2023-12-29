using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Transactions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class CreateUpdateTransferCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Required]
    public int SenderId { get; set; }

    public string SenderInstitutionImageUrl { get; set; }
    [Required]
    public int ReceiverId { get; set; }

    public string ReceiverInstitutionImageUrl { get; set; }
    [Required]
    public float Amount { get; set; }

    [Required]
    public DateTime? TransactionDate { get; set; } = DateTime.Now;
    public string Note { get; set; }
    public bool IsTransferAsExpense { get; set; } = true;

}

internal sealed class CreateUpdateTransferCommandHandler : IRequestHandler<CreateUpdateTransferCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private string _message;

    public CreateUpdateTransferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUpdateTransferCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if (!await Validate(command, cancellationToken))
            return result = await Result<int>.FailAsync(_message);

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Transfer>(command);
            await _unitOfWork.Repository<Transfer>().AddAsync(entity);

            
            var sender = await Credit(entity, cancellationToken);
            var receiver = await Debit(entity, cancellationToken);

            if(sender && receiver)
            {
                await _unitOfWork.Commit(cancellationToken);

                await ManageTransaction.AddAsync(_unitOfWork, entity.SenderId, TransactionType.Transfer.ToDescriptionString(),
                                                 entity.Created, entity.Amount, true, false, cancellationToken);

                await ManageTransaction.AddAsync(_unitOfWork, entity.ReceiverId, TransactionType.Transfer.ToDescriptionString(),
                                                entity.Created, entity.Amount, false, true, cancellationToken);

                result = await Result<int>.SuccessAsync(entity.Id, Messages.TransferSaved.ToDescriptionString());
            }
           else
            {              
                result = await Result<int>.FailAsync(_message);
            }
        }
        else
        {
               result = await UpdateTransfer(command, cancellationToken); 
        }

        return result;
    }

    private async Task<bool> Credit(Transfer transfer,CancellationToken cancellationToken)
    {
        var sender = await _unitOfWork.Repository<Account>().GetByIdAsync(transfer.SenderId);

        if(sender != null)
        {
            sender.Balance -= transfer.Amount;
            var entity = _mapper.Map<Account>(sender);
            await _unitOfWork.Repository<Account>().UpdateAsync(entity);

            return true;
        }

        _message = "";
        return false;
    }

    private async Task<bool> Debit(Transfer transfer, CancellationToken cancellationToken)
    {
        var receiver = await _unitOfWork.Repository<Account>().GetByIdAsync(transfer.ReceiverId);

        if (receiver != null)
        {
            receiver.Balance += transfer.Amount;
            var entity = _mapper.Map<Account>(receiver);
            await _unitOfWork.Repository<Account>().UpdateAsync(entity);

            return true;
        }

        return false;
    }

    private async Task<Result<int>> UpdateTransfer(CreateUpdateTransferCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        var transfer = await _unitOfWork.Repository<Transfer>().GetByIdAsync(command.Id);

        //disabling account's Sender and Receiver and amount for the meantime
        if (transfer != null)
        {
            //transfer.SenderId = (command.SenderId == 0) ? transfer.SenderId : command.SenderId;
            //transfer.ReceiverId = (command.ReceiverId == 0) ? transfer.ReceiverId : command.ReceiverId;
            //transfer.Amount = (command.Amount == 0) ? transfer.Amount : command.Amount;
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

        return result;
    }

    private async Task<bool> Validate(CreateUpdateTransferCommand command,CancellationToken cancellationToken)
    {
        if(command !=null)
        {
            if(command.SenderId == command.ReceiverId)
            {
                _message = "Sender Account must not be equal to the Receiver Account.";
                return false;
            }

            float sourceBalance = await GetAccountBalance(command.SenderId);
            if(sourceBalance < command.Amount) 
            {
                _message = "Sender's Balance is insufficient!";
                return false;
            }

            return true;
        }

        return false;
    }

    private async Task<float> GetAccountBalance(int accountId)
    {
        var account = await _unitOfWork.Repository<Account>().GetByIdAsync(accountId);

        return account.Balance;
    }
}