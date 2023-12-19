using Microsoft.EntityFrameworkCore;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Infrastructure.Contexts;

namespace ExpenseTrackerPro.Application.Infrastructure;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public RepositoryAsync(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
    {
        return await _context
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateAsync(T entity)
    {
        T? exist = _context.Set<T>().Find(entity.Id);
        _context.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }
}