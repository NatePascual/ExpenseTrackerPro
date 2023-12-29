using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class GetTransactionResponse  : IMapFrom<Transaction>
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string  AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string TransactionType { get; set; }
    public bool AsSender { get; set; } = false;
    public bool AsReceiver { get; set; } = false;
    public DateTime? TransactionDate { get; set; }
    public float Amount { get; set; }
    public string ImageUrl { get; set; }
    public string AmountToString { get; set; }
}

public class GetTransactionQuery : IRequest<GetTransactionView>
{
    public int Id { get; set; }

    public GetTransactionQuery(int id)
    {
        Id = id;
    }
}

public class GetTransactionView
{
    public Result<IList<GetTransactionResponse>> Transactions { get; set; }
}

internal sealed class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery,GetTransactionView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetTransactionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTransactionView> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {

        var filterSpec = new TransactionSpecification(request.Id);

        var getAll = await _unitOfWork.Repository<Transaction>().Entities
                     .AsNoTracking()
                     .Specify(filterSpec)
                     .ToListAsync();

        var map = _mapper.Map<IList<GetTransactionResponse>>(getAll);

        var result = new GetTransactionView();
        result.Transactions = await Result<IList<GetTransactionResponse>>.SuccessAsync(map);

        return result;
    }
}