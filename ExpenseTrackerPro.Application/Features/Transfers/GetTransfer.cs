using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Transfers;

public class GetTransferResponse : IMapFrom<Transfer>
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public float Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string Note { get; set; }
    public bool IsTransferAsExpense { get; set; }

    public string SenderInstitutionImageUrl { get; set; }

    public string ReceiverInstitutionImageUrl { get; set; }
    public string CurrencySymbol { get; set; }
}

public class GetTransferQuery : IRequest<GetTransferView>
{
    public string SearchString { get; set; }

    public GetTransferQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetTransferView
{
    public Result<IList<GetTransferResponse>> Transfers { get; set; }
}

internal sealed class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, GetTransferView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTransferQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetTransferView> Handle(GetTransferQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Transfer, GetTransferResponse>> expression = e => new GetTransferResponse()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            SenderName = e.Sender.Name,
            SenderInstitutionImageUrl = e.Sender.Institution.ImageUrl,
            ReceiverId = e.ReceiverId,
            ReceiverName = e.Receiver.Name,
            ReceiverInstitutionImageUrl = e.Receiver.Institution.ImageUrl,
            Amount = e.Amount,
            TransactionDate = e.TransactionDate,
            Note = e.Note,
            IsTransferAsExpense = false,
            CurrencySymbol = e.Receiver.Currency.Symbol
        };

        var filterSpec = new TransferSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<Transfer>().Entities
                     .AsNoTracking()
                     .Specify(filterSpec)
                     .Select(expression)
                     .ToListAsync();

        var map = _mapper.Map<IList<GetTransferResponse>>(getAll);

        var result = new GetTransferView();
        result.Transfers = await Result<IList<GetTransferResponse>>.SuccessAsync(map);

        return result;
    }
}