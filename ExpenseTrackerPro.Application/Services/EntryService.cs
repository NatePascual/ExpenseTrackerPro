using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using Microsoft.Identity.Client;

namespace ExpenseTrackerPro.Application.Services;

public  class EntryService : IEntryService
{
    public string Message { get; set; }

    public  async Task<bool> CreditAccount(IUnitOfWork unitOfWork,IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken )
    {
        return await ComputeAccount(unitOfWork, mapper, accountId, sourceAmount, true, cancellationToken);
    }

    public  async Task<bool> DebitAccount(IUnitOfWork unitOfWork, IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken)
    {
        return await ComputeAccount(unitOfWork, mapper, accountId, sourceAmount, false, cancellationToken);
    }

    private  async Task<bool> ComputeAccount(IUnitOfWork unitOfWork,IMapper mapper, int accountId, float sourceAmount, bool isCredit, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.Repository<Account>().GetByIdAsync(accountId);

        if (account == null)
        {
            Message = Messages.AccountDoesntExist.ToDescriptionString();
            return false;
        }

        if (isCredit)
        {
            account.Balance += sourceAmount;
        }
        else
        {
            account.Balance -= sourceAmount;
        }
       
        var entity = mapper.Map<Account>(account);
        await unitOfWork.Repository<Account>().UpdateAsync(entity);

        return true;
    }
}
