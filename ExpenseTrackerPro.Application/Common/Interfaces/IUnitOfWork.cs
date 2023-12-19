using ExpenseTrackerPro.Domain.Contracts;

namespace ExpenseTrackerPro.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepositoryAsync<T> Repository<T>() where T : BaseAuditableEntity;

    Task<int> Commit(CancellationToken cancellationToken);

    Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, string cacheKey);

    Task Rollback();
}
