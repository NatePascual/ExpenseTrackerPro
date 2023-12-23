using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Constants.Applications;
using ExpenseTrackerPro.Shared.Wrappers;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.AccountTypes;

public class GetAccountTypeResponse:IMapFrom<AccountType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Classification { get; set; }
    public string ImageUrl { get; set; }
}
public class GetAccountTypeQuery:IRequest<GetAccountTypeView>
{
    public string SearchString { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public GetAccountTypeQuery(int pageNumber, int pageSize, string searchString)
    {
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public class GetAccountTypeView
{
    public Result<List<GetAccountTypeResponse>> AccountTypes { get; set; }
}

internal class GetAccountTypeQueryHandler : IRequestHandler<GetAccountTypeQuery, GetAccountTypeView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    public GetAccountTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache)
    {
        _cache = cache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAccountTypeView> Handle(GetAccountTypeQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<AccountType, GetAccountTypeResponse>> expression = e => new GetAccountTypeResponse()
        {
            Id = e.Id,
            Name = e.Name,
            Classification = e.Classification,
            ImageUrl = e.ImageUrl
        };
        var filterSpec = new AccountTypeSpecification(request.SearchString);

        var getAll = () => _unitOfWork.Repository<AccountType>().Entities
                   .Specify(filterSpec)
                   .Select(expression)
                   .ToListAsync();

       var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetAccountTypesCacheKey, getAll);

        var map = _mapper.Map<List<GetAccountTypeResponse>>(list);

        var result = new GetAccountTypeView();
        result.AccountTypes = await Result<List<GetAccountTypeResponse>>.SuccessAsync(map);

        return result;
    }
}


