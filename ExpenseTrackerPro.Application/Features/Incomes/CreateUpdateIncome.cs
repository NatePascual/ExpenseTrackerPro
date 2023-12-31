using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Application.Features.Incomes;

public class CreateUpdateIncomeCommand: IRequest<Result<int>>
{
    public int Id { get; set; }
    [Required, Display(Name = "Income Category")]
    public int IncomeCategoryId { get; set; }

    [Required, Display(Name = "Account")]
    public int AccountId { get; set; }
    [Required]
    public float Amount { get; set; }
    [Required]
    public DateTime? TransactionDate { get; set; } = DateTime.Now;

    public string IncomeCategoryImageUrl { get; set; }
    public string InstitutionImageUrl { get; set; }

    public string Note { get; set; }
    public string Photo { get; set; }
}

internal sealed class CreateUpdateIncomeCommandHandler : IRequestHandler<CreateUpdateIncomeCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEntryService _entryService;
    private string _message;
    public CreateUpdateIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEntryService entryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _entryService = entryService;
    }

    public async Task<Result<int>> Handle(CreateUpdateIncomeCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Income>(command);
            await _unitOfWork.Repository<Income>().AddAsync(entity);

            var credit = await _entryService.DebitAccount(_unitOfWork,_mapper, entity.AccountId,entity.Amount, cancellationToken);

            if(credit)
            {
                await _unitOfWork.Commit(cancellationToken);

                result = await Result<int>.SuccessAsync(entity.Id, Messages.IncomeSaved.ToDescriptionString());
            }
            else
            {
                if (!_entryService.Message.IsNullOrEmpty())
                    _message = _entryService.Message;

                result = await Result<int>.FailAsync(_message);
            }

        }
        else
        {
            result = await UpdadteIncome(command,cancellationToken);
        }

        return result;
    }

    private async Task<Result<int>> UpdadteIncome(CreateUpdateIncomeCommand command,CancellationToken cancellationToken)
    {
        Result<int> result = null;

        var income = await _unitOfWork.Repository<Income>().GetByIdAsync(command.Id);

        if (income != null)
        {
            income.IncomeCategoryId = (command.IncomeCategoryId == 0) ? income.IncomeCategoryId : command.IncomeCategoryId;
            //income.AccountId = (command.AccountId == 0) ? income.AccountId : command.AccountId;
            //income.Amount = (command.Amount == 0) ? income.Amount : command.Amount;
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

        return result;
    }
}