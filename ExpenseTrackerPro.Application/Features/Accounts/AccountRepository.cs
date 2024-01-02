using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public interface IAccountRepository
{
    Task<bool> IsAccountUsed(int accountId);
}

public class AccountRepository: IAccountRepository
{
    private readonly IRepositoryAsync<Transfer> _transferRepository;
    private readonly IRepositoryAsync<Income> _incomeRepository;
    private readonly IRepositoryAsync<Expense> _expenseRepostory;
    private readonly IRepositoryAsync<JournalEntry> _journalEntryRepository;
    public AccountRepository(IRepositoryAsync<Transfer> transferRepository,
                             IRepositoryAsync<Income> incomeRepository,
                             IRepositoryAsync<Expense> expenseRepostory,
                             IRepositoryAsync<JournalEntry> journalEntryRepository)
    {
        _transferRepository = transferRepository;
        _incomeRepository = incomeRepository;
        _expenseRepostory = expenseRepostory;
        _journalEntryRepository = journalEntryRepository;
    }

    public async Task<bool> IsAccountUsed(int accountId)
    {
      var income =  await _incomeRepository.Entities.AnyAsync(x => x.AccountId == accountId);
      var expense = await _expenseRepostory.Entities.AnyAsync(x =>x.AccountId == accountId);
      var transfer = await _transferRepository.Entities.AnyAsync(x => x.SenderId == accountId || x.ReceiverId == accountId);
      var journalEntries = await _journalEntryRepository.Entities.AnyAsync(x => x.AccountId == accountId);
        return (income || transfer || expense || journalEntries);
    }
}
