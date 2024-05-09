using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public  class GetAccountByIdResponse : IMapFrom<Account>
{
    public int Id { get; set; }
    public int AccountTypeId { get; set; }
    public string AccountTypeName { get; set; }
    public string AccountTypeImageUrl { get; set; }
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set; }
    public string InstitutionImageUrl { get; set; }
    public int CurrencyId { get; set; }
    public string CurrencySymbol { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public float Balance { get; set; }
    public bool IsIncludedBalance { get; set; } = false;
    public DateTime? LastModified { get; set; }
    public IList<Expense> Expenses { get; set; } = new List<Expense>();
    public IList<Income> Incomes { get; set; } = new List<Income>();
}
public class GetAccountByIdQuery : IRequest<GetAccountByIdView>
{
    public int Id { get; set; }

    public GetAccountByIdQuery(int accountId)
    {
        Id = accountId;
    }
}

public class GetAccountByIdView
{
    public Result<GetAccountByIdResponse> Account {  get; set; }
}

internal sealed class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery,GetAccountByIdView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAccountByIdView> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Account, GetAccountByIdResponse>> expression = e => new GetAccountByIdResponse()
        {
            Id = e.Id,
            AccountTypeId = e.AccountTypeId,
            AccountTypeName = e.AccountType != null ? e.AccountType.Name : e.Institution.Name,
            AccountTypeImageUrl = e.AccountType != null ? e.AccountType.ImageUrl : e.Institution.ImageUrl,
            InstitutionId = e.InstitutionId,
            InstitutionName = e.Institution.Name,
            InstitutionImageUrl = e.Institution.ImageUrl,
            AccountNumber = e.AccountNumber,
            Name = e.Name,
            CurrencyId = e.CurrencyId,
            CurrencySymbol = e.Currency.Symbol,
            Balance = e.Balance,
            LastModified = e.LastModified
        };
        var getAccount = await _unitOfWork.Repository<Account>().Entities
                    .AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Select(expression)
                    .FirstOrDefaultAsync();
                   

        var map = _mapper.Map<GetAccountByIdResponse>(getAccount);
        map.Expenses = await GetExpense(request.Id);
        map.Incomes = await GetIncome(request.Id);

        var result = new GetAccountByIdView();
        result.Account = await Result<GetAccountByIdResponse>.SuccessAsync(map);

        return result;
    }

    private async Task<IList<Expense>> GetExpense(int accountId)
    {
        var getExpenses = await _unitOfWork.Repository<Expense>().Entities
                          .AsNoTracking()
                          .Where(x=> x.AccountId == accountId)
                          .ToListAsync();

        return getExpenses;
    }

    private async Task<IList<Income>> GetIncome(int accountId)
    {
        var getIncome = await _unitOfWork.Repository<Income>().Entities
                          .AsNoTracking()
                          .Where(x => x.AccountId == accountId)
                          .ToListAsync();

        return getIncome;
    }
}