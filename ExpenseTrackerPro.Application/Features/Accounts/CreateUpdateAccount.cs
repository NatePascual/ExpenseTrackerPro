﻿using ExpenseTrackerPro.Domain.Entities;
using MediatR;
using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Shared.Wrappers;
using System.ComponentModel.DataAnnotations;
using ExpenseTrackerPro.Application.Features.Transactions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class CreateUpdateAccountCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int AccountTypeId { get; set; }
    public int InstitutionId { get; set; }
    public int CurrencyId { get; set; }

    [Length(10, 30), Required]
    public string Name { get; set; }
    [MaxLength(4), Required]
    public string AccountNumber { get; set; }

    public float Balance { get; set; }

    public bool IsIncludedBalance { get; set; } = true;
}

internal sealed class CreateUpdateAccountCommandHandler : IRequestHandler<CreateUpdateAccountCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateUpdateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUpdateAccountCommand command, CancellationToken cancellationToken)
    {
        Result<int> result = null;

        if(command.Id == 0)
        {
            var entity = _mapper.Map<Account>(command);
            await _unitOfWork.Repository<Account>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            await ManageTransaction.AddAsync(_unitOfWork, entity.Id, "Starting Balance",
                                              DateOnly.FromDateTime(entity.Created), 
                                              entity.Balance, false, false,cancellationToken);

            result = await Result<int>.SuccessAsync(entity.Id, "Account Saved!");     
        }
        else
        {
            var account = await _unitOfWork.Repository<Account>().GetByIdAsync(command.Id);

            if(account != null)
            {
                account.AccountTypeId = (command.AccountTypeId == 0)  ? account.AccountTypeId : command.AccountTypeId;
                account.InstitutionId = (command.InstitutionId == 0) ? account.InstitutionId : command.InstitutionId;
                account.CurrencyId = (command.CurrencyId ==0) ? account.CurrencyId : command.CurrencyId;
                account.Name = command.Name ?? account.Name;
                account.AccountNumber = command.AccountNumber ?? account.AccountNumber;
                account.Balance = (command.Balance == 0) ? account.Balance : command.Balance;

                var entity = _mapper.Map<Account>(command);
                await _unitOfWork.Repository<Account>().UpdateAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
               
                result = await Result<int>.SuccessAsync(entity.Id, "Account Updated!");
            }
            else
            {
                result = await Result<int>.FailAsync("Record not found!");
            }
        }
        

        return result;

    }


}
