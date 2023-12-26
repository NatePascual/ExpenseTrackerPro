using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class GetAccountResponse : IMapFrom<Account>
{
    public int Id { get; set; }
    public int AccountTypeId { get; set; }
    public string AccountTypeName { get; set; }
    public string AccountTypeImageUrl {  get; set; }
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set;}
    public string InstitutionImageUrl {  get; set; }
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

    public GetAccountQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetAccountView
{
    public Result<List<GetAccountResponse>> Accounts { get; set; }
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
            AccountTypeId = e.AccountTypeId,
            AccountTypeName = e.Institution != null ? e.Institution.Name : e.AccountType.Name,
            AccountTypeImageUrl = e.Institution != null ? e.Institution.ImageUrl : e.AccountType.ImageUrl,
            InstitutionId = e.InstitutionId,
            InstitutionName = e.Institution.Name,
            InstitutionImageUrl = e.Institution.ImageUrl,
            AccountNumber = e.AccountNumber,
            Name = e.Name,
            CurrencyId = e.CurrencyId,
            CurrencySymbol = e.Currency.Symbol,
            Balance = e.Balance,
            IsIncludedBalance = e.IsIncludedBalance,
        };

        var filterSpec = new AccountSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<Account>().Entities
                  .Specify(filterSpec)
                  .Select(expression)
                  .ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetAccountResponse>>(getAll);

        var result = new GetAccountView();
        result.Accounts = await Result<List<GetAccountResponse>>.SuccessAsync(map); 

        return result;
    }
}