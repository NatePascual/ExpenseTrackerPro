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

namespace ExpenseTrackerPro.Application.Features.Institutions;

public class GetInstitutionResponse: IMapFrom<Institution>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl {  get; set; }
}

public class GetInstitutionQuery: IRequest<GetInstitutionView>
{
    public string SearchString { get; set; }
    public GetInstitutionQuery(string searchString)
    {
        SearchString = searchString;

    }
}

public class GetInstitutionView
{
    public Result<List<GetInstitutionResponse>> Institutions { get; set; }
}

internal class GetInstitutionQueryHandler : IRequestHandler<GetInstitutionQuery, GetInstitutionView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public GetInstitutionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache)
    {
        _cache = cache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetInstitutionView> Handle(GetInstitutionQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Institution, GetInstitutionResponse>> expression = e => new GetInstitutionResponse()
        {
            Id = e.Id,
            Name = e.Name,
            ImageUrl = e.ImageUrl
        };

        var filterSpec = new InstitutionSpecification(request.SearchString);

        Func<Task<List<GetInstitutionResponse>>> getAll = () => _unitOfWork.Repository<Institution>().Entities
                       .Specify(filterSpec)
                       .Select(expression)
                       .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetInstitutionCacheKey, getAll);

        var map = _mapper.Map<List<GetInstitutionResponse>>(list);

        var result = new GetInstitutionView();
        result.Institutions = await Result<List<GetInstitutionResponse>>.SuccessAsync(map);

        return result;

    }
}