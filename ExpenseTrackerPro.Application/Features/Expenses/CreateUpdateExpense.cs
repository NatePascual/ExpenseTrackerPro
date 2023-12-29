using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Features.Transactions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Application.Features.Expenses;

public class CreateUpdateExpenseCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int AccountId { get; set; }

    public string Provider {  get; set; }

    [Required]
    public DateTime? TransactionDate { get; set; } = DateTime.Now;

    [Required]
    public float Amount { get; set; }
    public string Note { get; set; }

    public string Photo { get; set; }

    public string CategoryImageUrl { get; set; }
    public string InstitutionImageUrl { get; set; }
}

internal sealed class CreateUpdateExpenseCommandHandler : IRequestHandler<CreateUpdateExpenseCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private string _message;

    public CreateUpdateExpenseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(CreateUpdateExpenseCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Expense>(command);
            await _unitOfWork.Repository<Expense>().AddAsync(entity);

            var credit = await Credit(entity, cancellationToken);

            if(credit)
            {
                await _unitOfWork.Commit(cancellationToken);

                await ManageTransaction.AddAsync(_unitOfWork, entity.AccountId, TransactionType.Expense.ToDescriptionString(),
                                                entity.Created, entity.Amount, true, false, cancellationToken);

                result = await Result<int>.SuccessAsync(entity.Id, Messages.ExpenseSaved.ToDescriptionString());
            }
            else
            {
                result = await Result<int>.FailAsync(_message);
            }
         
        }
        else
        {
            result = await UpdateExpense(command, cancellationToken);

        }
        return result;
    }

    private async Task<bool> Credit(Expense expense, CancellationToken cancellationToken)
    {
        var account = _unitOfWork.Repository<Account>().Entities.FirstOrDefault(x => x.Id == expense.AccountId);

        if (account != null)
        {
            account.Balance = account.Balance - expense.Amount;
            var entity = _mapper.Map<Account>(account);
            await _unitOfWork.Repository<Account>().UpdateAsync(entity);

            return true;
        }

        _message = Messages.AccountDoesntExist.ToDescriptionString();

        return false;
    }

    private async Task<Result<int>> UpdateExpense(CreateUpdateExpenseCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        var expense = await _unitOfWork.Repository<Expense>().GetByIdAsync(command.Id);

        //disabling Amount and Account for the meantime//
        if (expense != null)
        {
            //expense.AccountId = (command.AccountId == 0) ? expense.AccountId : command.AccountId;
            expense.CategoryId = (command.CategoryId == 0) ? expense.CategoryId : command.CategoryId;
            expense.Provider = command.Provider ?? expense.Provider;
            expense.TransactionDate = (command.TransactionDate != expense.TransactionDate) ? command.TransactionDate : expense.TransactionDate;
            expense.Note = command.Note ?? expense.Note;
            expense.Photo = command.Photo ?? expense.Photo;
            //expense.Amount = (command.Amount == 0) ? expense.Amount : command.Amount;

            var entity = _mapper.Map<Expense>(expense);
            await _unitOfWork.Repository<Expense>().UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, Messages.ExpenseUpdated.ToDescriptionString());
        }
        else
        {
            result = await Result<int>.FailAsync(Messages.RecordNotFound.ToDescriptionString());
        }

        return result;
    }
}
