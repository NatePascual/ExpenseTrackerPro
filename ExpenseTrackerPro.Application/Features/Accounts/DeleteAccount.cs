using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class DeleteAccountCommand: IRequest<Result<int>>
{
    public int Id {  get; private set; }

    public DeleteAccountCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _repository;
    public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IAccountRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task<Result<int>> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;
        var isAccountUsed = await _repository.IsAccountUsed(command.Id);

        if(!isAccountUsed)
        {
            var account = await _unitOfWork.Repository<Account>().GetByIdAsync(command.Id);
       
            await _unitOfWork.Repository<Account>().DeleteAsync(account);
  
            await _unitOfWork.Commit(cancellationToken);

            result = await Result<int>.SuccessAsync(account.Id, Messages.AccountDeleted.ToDescriptionString());
        }
        else
        {
            result = await Result<int>.FailAsync(Messages.DeletionNotAllowed.ToDescriptionString());
        }

        return result;
    }
}
