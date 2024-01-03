using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class GetTransactionResponse : IMapFrom<Transaction>
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }

    public List<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
}

public class GetJournalEntryResponse
{
   // SELECT
   //    T.TransactionDate,
   // A.Name + '-' +  AT.Name AS JournalEntryName,
   // CASE WHEN JE.IsDebit = 1 THEN -JE.Amount ELSE 0 END AS Debit,
   //    CASE WHEN JE.IsDebit = 0 THEN JE.Amount ELSE 0 END AS Credit,
   //    T.Description
   //FROM


   //    JournalEntries JE
   //JOIN
   //    Accounts A ON JE.AccountId = A.Id
   //JOIN
   //    Transactions T ON JE.TransactionId = T.Id
   //JOIN
   //    AccountTypes AT ON A.AccountTypeId = AT.Id



   //ORDER BY T.Id, JE.ID ASC

    public int Id { get; set;}
    public string AccountName { get; set;}
    public string AccountTypeName { get; set;}
    public float Debit { get; set; } = 0;
    public float Credit {  get; set; } = 0;
}
public class GetTransactionQuery : IRequest<GetTransactionView>
{
    public int TransactionId { get; set; }

    public GetTransactionQuery(int  transactionId)
    {
        TransactionId = transactionId;
    }
}

public class GetTransactionView()
{
    public Result<IList<GetTransactionResponse>> Transactions { get; set; }
}

internal sealed class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, GetTransactionView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetTransactionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetTransactionView> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var filterSpec = new TransactionSpecification(request.TransactionId);

        var getAll = _unitOfWork.Repository<Transaction>().Entities
                           .AsNoTracking()
                           .Specify(filterSpec)
                           .ToListAsync();

        var map = _mapper.Map<IList<GetTransactionResponse>>(getAll);

        var result = new GetTransactionView();

        result.Transactions = await Result<IList<GetTransactionResponse>>.SuccessAsync(map);

        return result;
    }
}