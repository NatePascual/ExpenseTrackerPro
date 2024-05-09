using AutoMapper;
using Azure.Core;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Radzen;
using System;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
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
    public ITrackerFilter Filter { get; set; }
    public string SearchString { get; set; }
    public GetAccountQuery(string searchString,ITrackerFilter filter=null)
    {
        Filter = filter;
        SearchString = searchString;
    }
}

public class GetAccountView
{
    public Result<List<GetAccountResponse>> Accounts { get; set; }
}

internal sealed class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountView>
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

        var filterSpec = new AccountSpecification(request.SearchString, await CreateFilter(request.Filter));

        // if (request.Filter != null)
        // {
        //     specification.AddCondition(e => e.AccountType.Name, (Operators)request.Filter.FirstOperator, request.Filter.FilterValue);
        // }
        // else if (request.Filter.FilterValue != null)
        // {
        //     //spec.AddCondition(e => e.AccountType.Name, (Operators)request.Filter.FirstOperator, request.Filter.FilterValue);
        //     //.AddCondition(e => e.AccountType.Name, (Operators)request.Filter.SecondOperator, request.Filter.SecondFilterValue)
        //     //.CombineWith((LogicalOperators)request.Filter.LogicalOperator);
        // }
        //// var result = myEntities.Where(specification.Build());

        var getAll = await _unitOfWork.Repository<Account>().Entities
                  .Include(a=>a.AccountType)
                  .Include(b=>b.Institution)
                  .Include(c=>c.Currency)
                  .Specify(filterSpec)
                  .Select(await CreateExpression())
                  .ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetAccountResponse>>(getAll);

        var result = new GetAccountView();
        result.Accounts = await Result<List<GetAccountResponse>>.SuccessAsync(map); 

        return result;
    }

    private async Task<Expression<Func<Account,GetAccountResponse>>> CreateExpression()
    {
        Expression<Func<Account, GetAccountResponse>> expression = e => new GetAccountResponse()
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
            IsIncludedBalance = e.IsIncludedBalance,
        };

        return expression;

    }

    private async Task<TrackerFilter> CreateFilter(ITrackerFilter filter)
    {
        TrackerFilter currentFilter = null;
        if (filter != null)
        {
            currentFilter = new TrackerFilter()
            {
                Property = filter.Property,
                FilterValue = filter.FilterValue,
                FirstOperator = filter.FirstOperator,
                LogicalOperator = filter.LogicalOperator,
                SecondFilterValue = filter.SecondFilterValue,
                SecondOperator = filter.SecondOperator,
            };
        }

        return currentFilter;
    }
}