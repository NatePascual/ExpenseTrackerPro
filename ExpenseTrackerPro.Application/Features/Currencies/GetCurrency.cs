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

namespace ExpenseTrackerPro.Application.Features.Currencies;

public class GetCurrency
{
}

public class GetCurrencyResponse : IMapFrom<Currency>
{
    public int Id { get; set; }
    public string CountryCurrency { get; set; }
    public string Code { get; set; }
    public string Symbol { get; set; }

}

public class GetCurrencyQuery : IRequest<GetCurrencyView>
{
    public string SearchString { get; set; }
    public GetCurrencyQuery(string searchString)
    {
        SearchString = searchString;    
    }
}

public class GetCurrencyView
{
    public Result<IList<GetCurrencyResponse>> Currencies { get; set; }
}

internal sealed class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery,GetCurrencyView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    public GetCurrencyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache)
    {
        _cache = cache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCurrencyView> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
    {
        var filterSpec = new CurrencySpecification(request.SearchString);

        var getAll = () => _unitOfWork.Repository<Currency>().Entities
                       .AsNoTracking()
                       .Specify(filterSpec)
                       .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetCurrenciesCacheKey, getAll);

        var map = _mapper.Map<IList<GetCurrencyResponse>>(list);

        var result = new GetCurrencyView();
        result.Currencies = await Result<IList<GetCurrencyResponse>>.SuccessAsync(map);

        return result;
    }
}