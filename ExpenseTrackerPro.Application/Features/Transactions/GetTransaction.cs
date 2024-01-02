using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Transactions;

public class GetTransactionResponse : IMapFrom<Transaction>
{

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