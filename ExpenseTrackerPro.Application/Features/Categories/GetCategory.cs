using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.ExpenseCategories;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Constants.Applications;
using ExpenseTrackerPro.Shared.Wrappers;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Categories;

public class GetCategoryResponse : IMapFrom<Category>
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; }
    public string Name { get; set; }
    public string ImageUrl {  get; set; }

}

public class GetCategoryQuery: IRequest<GetCategoryView>
{
    public string SearchString { get; set; }
    public GetCategoryQuery(string searchString)
    {
        SearchString = searchString;

    }
}

public class GetCategoryView
{
    public Result<IList<GetCategoryResponse>> Categories { get; set; }
}

internal sealed class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery,GetCategoryView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache)
    {
        _cache = cache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCategoryView> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var filterSpec = new CategorySpecification(request.SearchString);

        var getAll = () => _unitOfWork.Repository<Category>().Entities
                       .AsNoTracking()
                       .Specify(filterSpec)
                       .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetCategoriesCacheKey, getAll);

        var map = _mapper.Map<IList<GetCategoryResponse>>(list);

        var result = new GetCategoryView();
        result.Categories = await Result<IList<GetCategoryResponse>>.SuccessAsync(map);

        return result;

    }
}