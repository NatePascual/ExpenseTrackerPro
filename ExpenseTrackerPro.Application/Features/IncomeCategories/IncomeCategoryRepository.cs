using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.IncomeCategories;

public interface IIncomeCategoryRepository
{
    Task<bool> IsIncomeCategoryUsed(int incomeCategoryId);
}
public class IncomeCategoryRepository : IIncomeCategoryRepository
{
    private readonly IRepositoryAsync<IncomeCategory> _repository;
    public IncomeCategoryRepository(IRepositoryAsync<IncomeCategory> repository)
    {
        _repository = repository;
    }
    public async Task<bool> IsIncomeCategoryUsed(int incomeCategoryId)
    {
       return await _repository.Entities.AnyAsync(x=>x.Id == incomeCategoryId);
    }
}
