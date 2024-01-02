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

namespace ExpenseTrackerPro.Application.Features.IncomeCategories;

public class GetIncomeCategoryResponse  : IMapFrom<IncomeCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}

public class GetIncomeCategoryQuery : IRequest<GetIncomeCategoryView>
{
    public string SearchString { get; set; }

    public GetIncomeCategoryQuery(string searchString)
    {
            SearchString = searchString;
    }
}

public class GetIncomeCategoryView
{
    public Result<IList<GetIncomeCategoryResponse>> IncomeCategories {  get; set; }
}

internal sealed class GetIncomeCategoryQueryHandler : IRequestHandler<GetIncomeCategoryQuery, GetIncomeCategoryView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    public GetIncomeCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache)
    {
        _cache = cache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetIncomeCategoryView> Handle(GetIncomeCategoryQuery request, CancellationToken cancellationToken)
    {

        var filterSpec = new IncomeCategorySpecification(request.SearchString);

        var getAll = () => _unitOfWork.Repository<IncomeCategory>().Entities
                        .AsNoTracking()
                        .Specify(filterSpec)
                        .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetIncomeCategoriesCacheKey, getAll);

        var map = _mapper.Map<IList<GetIncomeCategoryResponse>>(list);

        var result = new GetIncomeCategoryView();
        result.IncomeCategories = await Result<IList<GetIncomeCategoryResponse>>.SuccessAsync(map);

        return result;
    }
}