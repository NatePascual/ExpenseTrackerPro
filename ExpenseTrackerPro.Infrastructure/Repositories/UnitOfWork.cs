using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Infrastructure.Contexts;
using LazyCache;
using System.Collections;

namespace ExpenseTrackerPro.Application.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ApplicationDbContext _context;
    private bool disposed;
    private Hashtable _repositories;
    private readonly IAppCache _cache;

    public UnitOfWork(ApplicationDbContext context, ICurrentUserService currentUserService, IAppCache cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUserService = currentUserService;
        _cache = cache;
    }

    public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : BaseAuditableEntity
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryAsync<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<TEntity>)_repositories[type];
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, string cacheKey)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(cacheKey);
        return result;
    }

    public Task Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _context.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }
}