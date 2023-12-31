using AutoMapper;

namespace ExpenseTrackerPro.Application.Common.Interfaces;

public interface IEntryService
{
    string Message { get; set; }
    Task<bool> DebitAccount(IUnitOfWork unitOfWork, IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken);
    Task<bool> CreditAccount(IUnitOfWork unitOfWork, IMapper mapper, int accountId, float sourceAmount, CancellationToken cancellationToken);

}
