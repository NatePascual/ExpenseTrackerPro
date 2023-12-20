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
using System.Linq.Expressions;

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
    public Result<List<GetCategoryResponse>> Categories { get; set; }
}

internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery,GetCategoryView>
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
        Expression<Func<Category, GetCategoryResponse>> expression = e => new GetCategoryResponse()
        {
            Id = e.Id,
            ParentId = e.ParentId,
            ParentName = e.Parent.Name,
            Name = e.Name,   
            ImageUrl = e.ImageUrl
        };

        var filterSpec = new CategorySpecification(request.SearchString);

        Func<Task<List<GetCategoryResponse>>> getAll = () => _unitOfWork.Repository<Category>().Entities
                       .Specify(filterSpec)
                       .Select(expression)
                       .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetCategoriesCacheKey, getAll);

        var map = _mapper.Map<List<GetCategoryResponse>>(list);

        var result = new GetCategoryView();
        result.Categories = await Result<List<GetCategoryResponse>>.SuccessAsync(map);

        return result;

    }
}