using AutoMapper;
using Azure.Core;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Expenses;
using ExpenseTrackerPro.Application.Features.Incomes;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public  class GetAccountByIdResponse : GetAccountResponse, IMapFrom<Account>
{
    public IList<GetExpenseResponse> OutgoingTransactions { get; set; }
    public IList<GetIncomeResponse> IncomingTransactions { get; set; }
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
            IncomingTransactions = GetIncome(e.Id).Result,
            OutgoingTransactions = GetExpense(e.Id).Result
        };
        var getAccount = _unitOfWork.Repository<Account>().Entities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

        var map = _mapper.Map<GetAccountByIdResponse>(getAccount);

        var result = new GetAccountByIdView();
        result.Account = await Result<GetAccountByIdResponse>.SuccessAsync(map);

        return result;
    }

    private async Task<IList<GetExpenseResponse>> GetExpense(int accountId)
    {
        var getExpenses = await _unitOfWork.Repository<Expense>().Entities
                          .AsNoTracking()
                          .Where(x=> x.AccountId == accountId)
                          .ToListAsync();

        return  _mapper.Map<IList<GetExpenseResponse>>(getExpenses);
    }

    private async Task<IList<GetIncomeResponse>> GetIncome(int accountId)
    {
        var getIncome = await _unitOfWork.Repository<Income>().Entities
                          .AsNoTracking()
                          .Where(x => x.AccountId == accountId)
                          .ToListAsync();

        return _mapper.Map<IList<GetIncomeResponse>>(getIncome);
    }
}