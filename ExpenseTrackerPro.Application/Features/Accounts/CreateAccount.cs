using ExpenseTrackerPro.Domain.Entities;
using MediatR;
using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Shared.Wrappers;

namespace ExpenseTrackerPro.Application.Features.Accounts;

//public class CreateAccountCommand:IRequest<Result<int>>
//{
//    public int AccountTypeId { get; set; }

//    public string? Provider { get; set; }

//    public string? Name { get; set; }

//    public string? AccountNumber { get; set; }

//    public string? Denomination { get; set; }

//    public decimal Amount { get; set; } = decimal.Zero;

//    public bool IsIncludedBalance { get; set; } = false;
//}

//internal sealed class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand,Result<int>>
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IMapper _mapper;
//    public CreateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
//    {
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//    }

//    public async Task<Result<int>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
//    {
//        var entity = _mapper.Map<Account>(command);

//        await _unitOfWork.Repository<Account>().AddAsync(entity);

//        await _unitOfWork.Commit(cancellationToken);

//        return await Result<int>.SuccessAsync(entity.Id,"Category Created");
//    }
//}
