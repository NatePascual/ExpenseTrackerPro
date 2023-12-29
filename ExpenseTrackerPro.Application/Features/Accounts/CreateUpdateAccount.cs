using ExpenseTrackerPro.Domain.Entities;
using MediatR;
using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Shared.Wrappers;
using System.ComponentModel.DataAnnotations;
using ExpenseTrackerPro.Application.Features.Transactions;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class CreateUpdateAccountCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    [Required, Display(Name ="Account Type")]
    public int AccountTypeId { get; set; }
    public string AccountTypeImageUrl { get; set; }

    [Required, Display(Name = "Bank Or Institution")]
    public int InstitutionId { get; set; }

    public string InstitutionImageUrl { get; set; }
    [Required, Display(Name = "Currency")]
    public int CurrencyId { get; set; }

    [Length(4, 30), Required]
    public string Name { get; set; }
    [Length(4,4, ErrorMessage ="Minimum Length is 4!"), Required, Display(Name = "Account Number")]
    public string AccountNumber { get; set; }

    [Required]
    public float Balance { get; set; }

    public bool IsIncludedBalance { get; set; } = true;
}

internal sealed class CreateUpdateAccountCommandHandler : IRequestHandler<CreateUpdateAccountCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private string _message;
    public CreateUpdateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUpdateAccountCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        var validate = await Validate(command, cancellationToken);

        if (!validate)
        {
           return await Result<int>.FailAsync(_message);
        }

        if (command.Id == 0)
        {
            var entity = _mapper.Map<Account>(command);
            await _unitOfWork.Repository<Account>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            await ManageTransaction.AddAsync(_unitOfWork, entity.Id, TransactionType.StartingBalance.ToDescriptionString(),
                                                entity.Created, entity.Balance, false, false, cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, Messages.AccountSaved.ToDescriptionString());
        }
        else
        {
            result = await UpdateAccount(command, cancellationToken);
        }

            
        return result;

    }

    private async Task<Result<int>> UpdateAccount(CreateUpdateAccountCommand command,CancellationToken cancellationToken)
    {
        Result<int> result = null;

        var account = await _unitOfWork.Repository<Account>().GetByIdAsync(command.Id);

        //Disabling balance for the meantime
        if (account != null)
        {
            account.AccountTypeId = (command.AccountTypeId == 0) ? account.AccountTypeId : command.AccountTypeId;
            account.InstitutionId = (command.InstitutionId == 0) ? account.InstitutionId : command.InstitutionId;
            account.CurrencyId = (command.CurrencyId == 0) ? account.CurrencyId : command.CurrencyId;
            account.Name = command.Name ?? account.Name;
            account.AccountNumber = command.AccountNumber ?? account.AccountNumber;
            //account.Balance = (command.Balance == 0) ? account.Balance : command.Balance;

            var entity = _mapper.Map<Account>(account);
            await _unitOfWork.Repository<Account>().UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, Messages.AccountUpdated.ToDescriptionString());
        }
        else
        {
            result = await Result<int>.FailAsync(Messages.RecordNotFound.ToDescriptionString());
        }

        return result;
    }

    private async Task<bool> Validate(CreateUpdateAccountCommand command, CancellationToken cancellationToken)
    {
        var account =  _unitOfWork.Repository<Account>().Entities
                       .Include(x=>x.Institution)
                       .FirstOrDefault(x => x.AccountNumber == command.AccountNumber 
                                         && x.InstitutionId == command.InstitutionId);

        if (account != null)
        {
            _message = $"Account with Institution {account.Institution.Name} and Number {account.AccountNumber} already exists!";
            return false;
        }
        return true;
    }
}
