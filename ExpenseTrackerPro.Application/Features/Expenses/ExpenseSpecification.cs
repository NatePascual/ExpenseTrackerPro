using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Expenses;

public class ExpenseSpecification : Specification<Expense>
{
    public ExpenseSpecification(string searchString)
    {
        Includes.Add(a => a.Account);
        Includes.Add(a => a.ExpenseCategory);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.Account != null &&  p.ExpenseCategory != null) && 
                            ( p.Provider.Contains(searchString) 
                              || p.Account.Name.Contains(searchString)
                              || p.ExpenseCategory.Name.Contains(searchString)
                              || p.Id == Convert.ToInt32(searchString)); 
        }
        else
        {
            Criteria = p => (p.Account != null && p.ExpenseCategory != null);
        }
    }
}
