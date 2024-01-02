using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.ExpenseCategories;

public interface ICategoryRepository
{
    Task<bool> IsCategoryUsed(int categoryId);
}
public class CategoryRepository: ICategoryRepository
{
    private readonly IRepositoryAsync<Category> _repository;
    public CategoryRepository(IRepositoryAsync<Category> repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsCategoryUsed(int categoryId)
    {
        return await _repository.Entities.AnyAsync(x => x.Id == categoryId);
    }
}

