using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class GetTransferResponse : IMapFrom<Transfer>
{
    public int Id { get; set; }
    public int FromAccountId { get; set; }
    public string FromAccountName { get; set; }
    public int ToAccountId { get; set; }
    public string ToAccountName { get; set; }
    public float Amount { get; set; }
    public DateOnly TransactionDate { get; set; }
    public string Note { get; set; }
    public bool IsTransferAsExpense { get; set; }
}

public class GetTransferQuery : IRequest<GetTransferView>
{
    public string SearchString { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetTransferQuery(int pageNumber, int pageSize, string searchString)
    {
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public class GetTransferView
{
    public Result<PaginatedResult<GetTransferResponse>> Transfers { get; set; }
}

internal class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, GetTransferView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTransferQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTransferView> Handle(GetTransferQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Transfer, GetTransferResponse>> expression = e => new GetTransferResponse()
        {
            Id = e.Id,
            FromAccountId = e.FromAccountId,
            FromAccountName = e.FromAccount.Name,
            ToAccountId = e.ToAccountId,
            ToAccountName = e.ToAccount.Name,
            Amount = e.Amount,
            TransactionDate = e.TransactionDate,
            Note = e.Note,
            IsTransferAsExpense = e.IsTransferAsExpense
        };

        var filterSpec = new TransferSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<Transfer>().Entities
                     .Specify(filterSpec)
                     .Select(expression)
                     .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        var map = _mapper.Map<PaginatedResult<GetTransferResponse>>(getAll);

        var result = new GetTransferView();
        result.Transfers = await Result<PaginatedResult<GetTransferResponse>>.SuccessAsync(map);

        return result;
    }
}