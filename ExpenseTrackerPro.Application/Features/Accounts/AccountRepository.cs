using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public interface IAccountRepository
{
    Task<bool> IsAccountUsed(int accountId);
}

public class AccountRepository: IAccountRepository
{
    private readonly IRepositoryAsync<Account> _repository;
    public AccountRepository(IRepositoryAsync<Account> repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsAccountUsed(int accountId)
    {
       return await _repository.Entities.AnyAsync(x=>x.Id == accountId);
    }
}
