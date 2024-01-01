using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using ExpenseTrackerPro.Shared.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTrackerPro.Application.Features.Transactions;
public class CreateTransaction : ICreateTransaction
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public string Message { get; set; }
    public CreateTransaction(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> ComputeAccount(int accountId, float sourceAmount, bool isDebit, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<Account>().GetByIdAsync(accountId);
        var result = true;

        if (account == null)
        {
            Message = Messages.AccountDoesntExist.ToDescriptionString();
            return false;
        }

        if (isDebit)
        {
            account.Balance -= sourceAmount;
        }
        else
        {
            account.Balance += sourceAmount;
        }

        try
        {
            var entity = _mapper.Map<Account>(account);
            await _unitOfWork.Repository<Account>().UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            result = true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            Message= Messages.DebitOrCreditError.ToDescriptionString();
            result = false;
        }
      
        return result;
    }

    public async Task<bool> CreateTransactionAccount(Account account, CancellationToken cancellationToken)
    {
        var result = true;
        var initialAccount = await _unitOfWork.Repository<Account>().Entities.FirstOrDefaultAsync(x=>x.Id == Convert.ToInt32(DefaultAccount.Initial));
        var description = $"New Account : {account.Name} with Number: {account.AccountNumber}";

        if (initialAccount == null)
        {
            Message = Messages.AccountDoesntExist.ToDescriptionString();
            return false;
        }

        var transaction = new CreateTransactionCommand(DateTime.Now, description);
        transaction.Entries.AddRange(await CreateJournalEntries(initialAccount.Id, account.Id, transaction.Id, account.Balance, true));

        try
        {
            var entity = _mapper.Map<Transaction>(transaction);
            await _unitOfWork.Repository<Transaction>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            result = true;
        }
        catch(Exception ex)
        {
            await _unitOfWork.Rollback();
            Message = Messages.AccountTransactionError.ToDescriptionString();
            result = false;
        }

        return result;
    }

    public async Task<bool> CreateTransactionTransfer(Transfer transfer, CancellationToken cancellationToken)
    {
        var result = true;
        var senderAccount = await _unitOfWork.Repository<Account>().GetByIdAsync(transfer.SenderId);
        var receiverAccount = await _unitOfWork.Repository<Account>().GetByIdAsync(transfer.ReceiverId);

        if (senderAccount == null)
        {
            Message = $"Sender {Messages.AccountDoesntExist.ToDescriptionString()}";
            return false;
        }

        if (receiverAccount == null)
        {
            Message = $"Receiver {Messages.AccountDoesntExist.ToDescriptionString()}";
            return false;
        }

        var transaction = new CreateTransactionCommand(transfer.TransactionDate.Value, $"New Transfer from Account  {senderAccount.Name} to Account  {receiverAccount.Name}");
        transaction.Entries.AddRange(await CreateJournalEntries(transfer.SenderId, transfer.ReceiverId, transaction.Id, transfer.Amount));

        try
        {    
            var entity = _mapper.Map<Transaction>(transaction);
            await _unitOfWork.Repository<Transaction>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            result=true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            Message = Messages.TransferTransactionError.ToDescriptionString();
            result = false;
        }
     
        var sender = await ComputeAccount(senderAccount.Id, transfer.Amount, true, cancellationToken);
        var receiver = await ComputeAccount(receiverAccount.Id, transfer.Amount, false, cancellationToken);

        if(sender && receiver)
            result = true;

        if (!sender || !receiver)
            result = false;

        return result;
    }

    public async Task<bool> CreateTransactionIncome(Income income, CancellationToken cancellationToken)
    {
        var result = true;
        var incomeAccount = await _unitOfWork.Repository<Account>().GetByIdAsync(Convert.ToInt32(DefaultAccount.Income));
        var account = await _unitOfWork.Repository<Account>().GetByIdAsync(income.AccountId);

        var desciption = !income.Note.IsNullOrEmpty() ? income.Note : $"New Income for the account { account.Name } with { income.Amount }";

        if (incomeAccount == null)
        {
            Message = $"Income {Messages.AccountDoesntExist.ToDescriptionString()}";
            return false;
        }

        var transaction = new CreateTransactionCommand(income.TransactionDate.Value, desciption);
        transaction.Entries.AddRange(await CreateJournalEntries(incomeAccount.Id, account.Id, transaction.Id, income.Amount));

        try
        {
            var entity = _mapper.Map<Transaction>(transaction);
            await _unitOfWork.Repository<Transaction>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            result = true;
        }
       catch(Exception ex)
        {
            Message = Messages.IncomeTransactionError.ToDescriptionString();
            result = false;
        }

         result = await ComputeAccount(account.Id, income.Amount, false, cancellationToken);

        return result;
    }
    public async Task<bool> CreateTransactionExpense(Expense expense, CancellationToken cancellationToken)
    {
        var result = true;
        var expenseAccount = await _unitOfWork.Repository<Account>().GetByIdAsync(Convert.ToInt32(DefaultAccount.Expense));
        var account = await _unitOfWork.Repository<Account>().GetByIdAsync(expense.AccountId);

        var desciption = !expense.Note.IsNullOrEmpty() ? expense.Note : $"New Expense for the account {account.Name} with { expense.Provider }";

        if (expenseAccount == null)
        {
            Message = $"Expense {Messages.AccountDoesntExist.ToDescriptionString()}";
            return false;
        }

        var transaction = new CreateTransactionCommand(expense.TransactionDate.Value, desciption);
        transaction.Entries.AddRange(await CreateJournalEntries(expenseAccount.Id, account.Id, transaction.Id, expense.Amount));

        try
        {
            var entity = _mapper.Map<Transaction>(transaction);
            await _unitOfWork.Repository<Transaction>().AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            result = true;
        }
        catch(Exception ex)
        {
            await _unitOfWork.Rollback();
            Message = Messages.ExpenseTransactionError.ToDescriptionString();
            result = false;
        }

        result = await ComputeAccount(account.Id, expense.Amount, true, cancellationToken);

        return true;
    }
    private async Task<List<JournalEntry>> CreateJournalEntries(int debitAccountId, int creditAccountId, int transactionId, float amount, bool isInitial = false)
    {
        List<JournalEntry> journalEntries = new List<JournalEntry>();
        var balance = isInitial ? 0 : amount;

        journalEntries.Add(new JournalEntry(debitAccountId, transactionId, balance, true));
        journalEntries.Add(new JournalEntry(creditAccountId, transactionId, amount, false));

        return await Task.FromResult(journalEntries);
    }
}
public class CreateTransactionCommand
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public List<JournalEntry> Entries { get; set; } = new List<JournalEntry>();

    public CreateTransactionCommand(DateTime transactionDate, string description)
    {
        TransactionDate = transactionDate;
        Description = description;
    }

}