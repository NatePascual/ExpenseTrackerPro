using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Expenses;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Incomes;

public class GetIncomeResponse : IMapFrom<Income>
{
    public int Id { get; set; }
    public int IncomeCategoryId { get; set; }
    public string IncomeCategoryName { get; set; }
    public string IncomeCategoryImageUrl { get; set; }
    public string InstitutionImageUrl { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string CurrencySymbol { get; set; }
    public float Amount { get; set; }
    public string Note {  get; set; }
    public string Photo {  get; set; }
}

public class GetIncomeQuery : IRequest<GetIncomeView>
{
    public string SearchString { get; set; }

    public GetIncomeQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetIncomeView
{
    public Result<List<GetIncomeResponse>> Incomes {  get; set; }
}

internal sealed class GetIncomeQueryHandler : IRequestHandler<GetIncomeQuery,GetIncomeView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetIncomeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetIncomeView> Handle(GetIncomeQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Income, GetIncomeResponse>> expression = e => new GetIncomeResponse()
        {
            Id = e.Id,
            AccountId = e.AccountId,
            AccountName = e.Account.Name,
            IncomeCategoryId = e.IncomeCategoryId,
            IncomeCategoryName = e.IncomeCategory.Name,
            IncomeCategoryImageUrl = e.IncomeCategory.ImageUrl,
            InstitutionImageUrl = e.Account.Institution.ImageUrl,
            CurrencySymbol = e.Account.Currency.Symbol,
            TransactionDate = e.TransactionDate,
            Amount = e.Amount,
            Note = e.Note,
            Photo = e.Photo
        };
        var filterSpec = new IncomeSpecification(request.SearchString);
        var getAll = await _unitOfWork.Repository<Income>().Entities
                     .AsNoTracking()
                     .Specify(filterSpec)
                     .Select(expression)
                     .ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetIncomeResponse>>(getAll);

        var result = new GetIncomeView();
        result.Incomes = await Result<List<GetIncomeResponse>>.SuccessAsync(map);

        return result;
    }
}