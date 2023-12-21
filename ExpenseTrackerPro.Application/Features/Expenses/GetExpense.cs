using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Expenses;

public class GetExpenseResponse: IMapFrom<Expense>
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public string Provider {  get; set; }
    public string Title { get; set; }
    public DateOnly TransactionDate { get; set; }
    public string Note {  get; set; }
    public string Photo { get; set; }
}

public class GetExpenseQuery : IRequest<GetExpenseView>
{
    public string SearchString { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetExpenseQuery(int pageNumber, int pageSize, string searchString)
    {
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public class GetExpenseView
{
    public Result<PaginatedResult<GetExpenseResponse>> Expenses { get; set; }
}

internal class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, GetExpenseView>
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
            AccountId = e.AccountId,
            AccountName = e.Account.Name,
            Provider = e.Provider,
            TransactionDate = e.TransactionDate,
            Note = e.Note,
            Photo = e.Photo
        };

        var filterSpec = new ExpenseSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<Expense>().Entities
                  .Specify(filterSpec)
                  .Select(expression)
                  .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        var map = _mapper.Map<PaginatedResult<GetExpenseResponse>>(getAll);

        var result = new GetExpenseView();
        result.Expenses = await Result<PaginatedResult<GetExpenseResponse>>.SuccessAsync(map);

        return result;
    }
}