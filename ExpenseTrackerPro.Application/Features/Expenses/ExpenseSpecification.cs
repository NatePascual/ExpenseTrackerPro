using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            ( p.Provider.Contains(searchString) || p.Account.Name.Contains(searchString) || p.ExpenseCategory.Name.Contains(searchString)); 
        }
        else
        {
            Criteria = p => (p.Account != null && p.ExpenseCategory != null);
        }
    }
}
