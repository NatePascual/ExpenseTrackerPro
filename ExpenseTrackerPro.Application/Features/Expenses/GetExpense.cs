using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Expenses;

public class GetExpenseResponse: IMapFrom<Expense>
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryImageUrl {  get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public string InstitutionImageUrl { get; set; }
    public string Provider {  get; set; }
    public DateTime? TransactionDate { get; set; }
    public string CurrencySymbol { get; set; }
    public float Amount { get; set; }
    public string Note {  get; set; }
    public string Photo { get; set; }
}

public class GetExpenseQuery : IRequest<GetExpenseView>
{
    public string SearchString { get; set; }

    public GetExpenseQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetExpenseView
{
    public Result<IList<GetExpenseResponse>> Expenses { get; set; }
}

internal sealed class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, GetExpenseView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetExpenseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetExpenseView> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Expense, GetExpenseResponse>> expression = e => new GetExpenseResponse()
        {
            Id = e.Id,
            CategoryId = e.CategoryId,
            CategoryName = e.ExpenseCategory.Name,
            CategoryImageUrl = e.ExpenseCategory.ImageUrl,
            AccountId = e.AccountId,
            AccountName = e.Account.Name,
            InstitutionImageUrl = e.Account.Institution.ImageUrl,
            CurrencySymbol = e.Account.Currency.Symbol,
            Provider = e.Provider,
            TransactionDate = e.TransactionDate,
            Amount = e.Amount,
            Note= e.Note,
            Photo = e.Photo
        };
        var filterSpec = new ExpenseSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<Expense>().Entities
                          .AsNoTracking()
                          .Specify(filterSpec)
                          .Select(expression)
                          .ToListAsync(cancellationToken);

        var map = _mapper.Map<IList<GetExpenseResponse>>(getAll);

        var result = new GetExpenseView();
        result.Expenses = await Result<IList<GetExpenseResponse>>.SuccessAsync(map);

        return result;
    }
}