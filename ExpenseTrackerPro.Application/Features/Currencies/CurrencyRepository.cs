
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Currencies;

public interface ICurrencyRepository
{
    Task<bool> IsCurrecyUsed(int currencyId);
}
public class CurrencyRepository: ICurrencyRepository
{
    private readonly IRepositoryAsync<Currency> _repository;
    public CurrencyRepository(IRepositoryAsync<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsCurrecyUsed(int currencyId)
    {
       return await _repository.Entities.AnyAsync(x => x.Id == currencyId);
    }
}
