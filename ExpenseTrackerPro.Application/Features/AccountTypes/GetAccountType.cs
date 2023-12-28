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

    public GetAccountTypeQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetAccountTypeView
{
    public Result<IList<GetAccountTypeResponse>> AccountTypes { get; set; }
}

internal sealed class GetAccountTypeQueryHandler : IRequestHandler<GetAccountTypeQuery, GetAccountTypeView>
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

        var filterSpec = new AccountTypeSpecification(request.SearchString);

        var getAll = () => _unitOfWork.Repository<AccountType>().Entities
                   .AsNoTracking()
                   .Specify(filterSpec)
                   .ToListAsync();

       var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetAccountTypesCacheKey, getAll);

        var map = _mapper.Map<IList<GetAccountTypeResponse>>(list);

        var result = new GetAccountTypeView();
        result.AccountTypes = await Result<IList<GetAccountTypeResponse>>.SuccessAsync(map);

        return result;
    }
}


