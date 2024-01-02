using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Features.BankOrInstitutions;

public interface IInstitutionRepository
{
    Task<bool> IsInstitutionUsed(int institutionId);
}
public class InstitutionRepository: IInstitutionRepository
{
    private readonly IRepositoryAsync<Institution> _repository;
    public InstitutionRepository(IRepositoryAsync<Institution> repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsInstitutionUsed(int institutionId)
    {
       return await _repository.Entities.AnyAsync(x => x.Id == institutionId);
    }
}
