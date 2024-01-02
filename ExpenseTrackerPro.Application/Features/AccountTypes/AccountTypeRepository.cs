using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.AccountTypes;
public interface IAccountTypeRepository
{
    Task<bool> IsAccountTypeUsed(int accountTypeId);
}

public class AccountTypeRepository: IAccountTypeRepository
{
    private readonly IRepositoryAsync<AccountType> _repository;
    public AccountTypeRepository(IRepositoryAsync<AccountType> repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsAccountTypeUsed(int accountTypeId)
    {
        return await _repository.Entities.AnyAsync(x =>x.Id == accountTypeId);
    }
}
