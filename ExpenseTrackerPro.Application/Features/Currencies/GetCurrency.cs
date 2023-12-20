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
    public Result<List<GetCurrencyResponse>> Currencies { get; set; }
}

internal class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery,GetCurrencyView>
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
        Expression<Func<Currency, GetCurrencyResponse>> expression = e => new GetCurrencyResponse()
        {
            Id = e.Id,
            CountryCurrency = e.CountryCurrency,
            Code = e.Code,
            Symbol = e.Symbol
        };

        var filterSpec = new CurrencySpecification(request.SearchString);

        Func<Task<List<GetCurrencyResponse>>> getAll = () => _unitOfWork.Repository<Currency>().Entities
                       .Specify(filterSpec)
                       .Select(expression)
                       .ToListAsync();

        var list = await _cache.GetOrAddAsync(ApplicationConstant.Cache.GetCurrenciesCacheKey, getAll);

        var map = _mapper.Map<List<GetCurrencyResponse>>(list);

        var result = new GetCurrencyView();
        result.Currencies = await Result<List<GetCurrencyResponse>>.SuccessAsync(map);

        return result;
    }
}