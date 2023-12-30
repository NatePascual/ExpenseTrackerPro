using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using Microsoft.Identity.Client;

namespace ExpenseTrackerPro.Application.Services;

public static class UtilityService
{
    public static string Message {  get; set; }
    public static async Task<bool> Credit(IUnitOfWork unitOfWork,IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken )
    {
        return await ComputeAccount(unitOfWork, mapper, accountId, sourceAmount, true, cancellationToken);
    }

    public static async Task<bool> Debit(IUnitOfWork unitOfWork, IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken)
    {
        return await ComputeAccount(unitOfWork, mapper, accountId, sourceAmount, false, cancellationToken);
    }

    private static async Task<bool> ComputeAccount(IUnitOfWork unitOfWork,IMapper mapper, int accountId, float sourceAmount, bool isCredit, CancellationToken)
    {
        var account = await unitOfWork.Repository<Account>().GetByIdAsync(accountId);

        if (account == null)
        {
            Message = Messages.AccountDoesntExist.ToDescriptionString();
            return false;
        }

        if (isCredit)
        {
            account.Balance -= sourceAmount;
        }
        else
        {
            account.Balance += sourceAmount;
        }
       
        var entity = mapper.Map<Account>(account);
        await unitOfWork.Repository<Account>().UpdateAsync(entity);

        return true;
    }
}
