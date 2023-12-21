using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.AccountTypes;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using LazyCache;
using MediatR;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class GetAccountResponse : IMapFrom<Account>
{
    public int Id { get; set; }
    public int AccountTypeId { get; set; }
    public string AccountTypeName { get; set; }
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set;}
    public int CurrencyId {  get; set; }
    public string CurrencySymbol { get; set; }
    public string Name { get; set; }
    public string AccountNumber {  get; set; }
    public float Balance { get; set; }
    public bool IsIncludedBalance { get; set; } = false;
}

public class GetAccountQuery : IRequest<GetAccountView>
{
    public string SearchString { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetAccountQuery(int pageNumber, int pageSize, string searchString)
    {
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public class GetAccountView
{
    public Result<PaginatedResult<GetAccountResponse>> Accounts { get; set; }
}

internal class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAccountView> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Account, GetAccountResponse>> expression = e => new GetAccountResponse()
        {
            Id = e.Id,
            InstitutionId = e.InstitutionId,
            InstitutionName = e.Institution.Name,
            CurrencyId = e.CurrencyId,
            CurrencySymbol = e.Currency.Symbol,
            Name = e.Name,
            AccountNumber = e.AccountNumber,
            Balance = e.Balance,
            IsIncludedBalance = e.IsIncludedBalance
        };

        var filterSpec = new AccountSpecification(request.SearchString);

        var getAll =  await _unitOfWork.Repository<Account>().Entities
                  .Specify(filterSpec)
                  .Select(expression)
                  .ToPaginatedListAsync(request.PageNumber,request.PageSize);

        var map = _mapper.Map<PaginatedResult<GetAccountResponse>>(getAll);

        var result = new GetAccountView();
        result.Accounts = await Result<PaginatedResult<GetAccountResponse>>.SuccessAsync(map); 

        return result;
    }
}