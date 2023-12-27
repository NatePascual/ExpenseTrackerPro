using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Transactions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Application.Features.Incomes;

public class CreateUpdateIncomeCommand: IRequest<Result<int>>
{
    public int Id { get; set; }
    [Required]
    public int IncomeCategoryId { get; set; }
    [Required]
    public int AccountId { get; set; }
    [Required]
    public float Amount { get; set; }
    [Required]
    public DateTime? TransactionDate { get; set; } = DateTime.Now;
    public string Note { get; set; }
    public string Photo { get; set; }
}

internal sealed class CreateUpdateIncomeCommandHandler : IRequestHandler<CreateUpdateIncomeCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUpdateIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUpdateIncomeCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Income>(command);
            await _unitOfWork.Repository<Income>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            await ManageTransaction.AddAsync(_unitOfWork, entity.AccountId,TransactionType.Income.ToDescriptionString(),
                                            entity.Created,entity.Amount, false, true, cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, Messages.IncomeSaved.ToDescriptionString());

        }
        else
        {
            var income = await _unitOfWork.Repository<Income>().GetByIdAsync(command.Id);

            if(income != null)
            {
                income.IncomeCategoryId = (command.IncomeCategoryId == 0) ? income.IncomeCategoryId : command.IncomeCategoryId;
                income.AccountId = (command.AccountId == 0) ? income.AccountId : command.AccountId;
                income.Amount = (command.Amount == 0) ? income.Amount : command.Amount;
                income.TransactionDate = (command.TransactionDate != income.TransactionDate) ? command.TransactionDate : income.TransactionDate;
                income.Note = command.Note ?? income.Note;
                income.Photo = command.Photo ?? income.Photo;

                var entity = _mapper.Map<Income>(income);
                await _unitOfWork.Repository<Income>().UpdateAsync(entity);
                await _unitOfWork.Commit(cancellationToken);

                result = await Result<int>.SuccessAsync(entity.Id, Messages.IncomeUpdated.ToDescriptionString());
            }
            else
            {
                result = await Result<int>.FailAsync(Messages.RecordNotFound.ToDescriptionString());
            }
        }

        return result;
    }
}